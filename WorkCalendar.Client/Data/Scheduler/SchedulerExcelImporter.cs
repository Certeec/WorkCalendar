using ClosedXML.Excel;
using Models.Enums;
using System.Text.RegularExpressions;

namespace WorkCalendar.Client.Data.Scheduler
{
    public class SchedulerExcelImporter
    {
        public static List<string> GetEmployeeNames(Stream excelStream)
        {
            var names = new HashSet<string>();
            using var workbook = new XLWorkbook(excelStream);
            var worksheet = workbook.Worksheets.First();
            
            for (int r = 1; r <= worksheet.LastRowUsed()?.RowNumber(); r++)
            {
                var row = worksheet.Row(r);
                if (row.IsEmpty()) continue;

                string employeeName = row.Cell(2).GetString()?.Trim();
                if (!string.IsNullOrWhiteSpace(employeeName) && employeeName.Length > 3)
                {
                    names.Add(employeeName);
                }
            }

            return names.ToList();
        }

        public static List<SchedulerTask> ParseTasks(Stream excelStream, string targetEmployeeName, bool simplifyPlaceNames, SchedulerTaskState defaultState)
        {
            var tasks = new List<SchedulerTask>();
            using var workbook = new XLWorkbook(excelStream);
            var worksheet = workbook.Worksheets.First();

            var dateColumns = new Dictionary<int, DateTime>();
            string currentPlace = "";

            for (int r = 1; r <= worksheet.LastRowUsed()?.RowNumber(); r++)
            {
                var row = worksheet.Row(r);
                if (row.IsEmpty()) continue;

                bool isDateRow = false;
                for (int c = 3; c <= 15; c++)
                {
                    if (row.Cell(c).TryGetValue<DateTime>(out var checkD) && checkD.Year > 2000)
                    {
                        isDateRow = true;
                        break;
                    }
                }

                if (isDateRow)
                {
                    dateColumns.Clear();
                    for (int c = 3; c <= worksheet.LastColumnUsed()?.ColumnNumber(); c++)
                    {
                        var cell = row.Cell(c);
                        if (cell.TryGetValue<DateTime>(out var date) && date.Year > 2000)
                        {
                            dateColumns[c] = date;
                        }
                    }
                    continue; 
                }

                var placeCellVal = row.Cell(1).GetString()?.Trim();
                if (!string.IsNullOrWhiteSpace(placeCellVal))
                {
                    currentPlace = placeCellVal;
                }

                string employeeName = row.Cell(2).GetString()?.Trim();
                if (string.Equals(employeeName, targetEmployeeName, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var kvp in dateColumns)
                    {
                        int colIndex = kvp.Key;
                        DateTime date = kvp.Value;
                        string cellValue = row.Cell(colIndex).GetString()?.Trim();

                        if (string.IsNullOrWhiteSpace(cellValue))
                            continue;

                        if (cellValue.Equals("OFF", StringComparison.OrdinalIgnoreCase) ||
                            cellValue.Equals("URLOP", StringComparison.OrdinalIgnoreCase) ||
                            cellValue.Equals("L4", StringComparison.OrdinalIgnoreCase) ||
                            cellValue.Equals("UW", StringComparison.OrdinalIgnoreCase))
                        {
                            tasks.Add(new SchedulerTask
                            {
                                DateStart = date.Date,
                                DateEnd = date.Date.AddDays(1).AddSeconds(-1),
                                Place = cellValue.ToUpper(),
                                TaskState = SchedulerTaskState.Unavailabe,
                                IsActive = true,
                                ShowDetails = true
                            });
                            continue;
                        }

                        var match = Regex.Match(cellValue, @"(\d{1,2})(?:[:.,](\d{1,2}))?\s*-+\s*(\d{1,2})(?:[:.,](\d{1,2}))?");
                        if (match.Success)
                        {
                            if (int.TryParse(match.Groups[1].Value, out int startHour) &&
                                int.TryParse(match.Groups[3].Value, out int endHour))
                            {
                                int startMin = 0;
                                if (match.Groups[2].Success) int.TryParse(match.Groups[2].Value, out startMin);

                                int endMin = 0;
                                if (match.Groups[4].Success) int.TryParse(match.Groups[4].Value, out endMin);

                                var dateStart = date.Date.AddHours(startHour).AddMinutes(startMin);
                                var dateEnd = date.Date.AddHours(endHour).AddMinutes(endMin);

                                if (dateEnd <= dateStart)
                                {
                                    dateEnd = dateEnd.AddDays(1);
                                }

                                string placeName = currentPlace;
                                if (!simplifyPlaceNames)
                                {
                                    placeName = cellValue.Replace(match.Value, "").Trim();
                                    if (string.IsNullOrWhiteSpace(placeName))
                                        placeName = currentPlace;
                                }

                                tasks.Add(new SchedulerTask
                                {
                                    DateStart = dateStart,
                                    DateEnd = dateEnd,
                                    Place = placeName,
                                    TaskState = defaultState,
                                    IsActive = true,
                                    ShowDetails = true
                                });
                            }
                        }
                    }
                }
            }

            return tasks;
        }
    }
}

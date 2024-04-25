﻿using BookShopOnline.Core.Interfaces.Excel;
using BookShopOnline.Core.Resources;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Excel
{
    public abstract class ExcelService<TDto> : IExcelService<TDto>
    {
        public byte[] ExportExcelAsync(IEnumerable<TDto> data, List<string>? columns)
        {
            var properties = typeof(TDto).GetProperties();
            var stream = new MemoryStream();
            using (var excelPackage = new ExcelPackage(stream))
            {
                // đặt tên người tạo file
                excelPackage.Workbook.Properties.Author = "LQHUY";

                var worksheet = excelPackage.Workbook.Worksheets.Add("Báo cáo doanh thu");

                // set style mặc định cho toàn bộ file
                worksheet.Cells.Style.Font.Size = 14;
                worksheet.Cells.Style.Font.Name = "Times New Roman";

                // lấy ra số lượng cột cần dùng dựa vào số lượng header
                var countColHeader = columns.Count();

                // gán giá trị cho cell vừa merge
                worksheet.Cells[1, 1].Value = string.Format("Báo cáo doanh thu");
                // merge các column lại từ column 1 đến số column header và set style
                worksheet.Cells[1, 1, 2, countColHeader + 1].Merge = true;
                worksheet.Cells[1, 1, 2, countColHeader + 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1, 2, countColHeader + 1].Style.Font.Size = 24;
                worksheet.Cells[1, 1, 2, countColHeader + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var headerCol = 2;
                var headerRow = 3;

                worksheet.Cells[headerRow, 1].Value = "STT";

                //tạo các header từ column header đã tạo từ bên trên
                foreach (var column in columns)
                {
                    foreach (var property in properties)
                    {
                        if (property.Name == column)
                        {
                            //gán giá trị
                            var displayNameAttribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true) as DisplayNameAttribute[];
                            worksheet.Cells[headerRow, headerCol].Value = displayNameAttribute[0].DisplayName;
                            headerCol++;
                        }
                    }
                }


                //set style cho header
                var cells = worksheet.Cells[headerRow, 1, headerRow, countColHeader + 1];
                //set màu thành gray
                var fill = cells.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                //căn chỉnh các border
                var border = cells.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                    border.Left.Style =
                    border.Right.Style = ExcelBorderStyle.Thin;

                SetCellValueEnterFileExcel(data, columns, worksheet, properties);

                excelPackage.Save();

                //chuyển đổi nội dung của đối tượng stream thành một mảng byte.
                var bytes = stream.ToArray();
                return bytes;

            }
        }



        private void SetCellValueEnterFileExcel(IEnumerable<TDto> data, List<string> columns, ExcelWorksheet worksheet, PropertyInfo[] properties)
        {
            var currentRow = 4;
            var colSTT = 1;
            var totalCol = properties.ToList().Count;
            //gán giá trị cho từng dòng và cột
            foreach (var item in data)
            {
                worksheet.Cells[currentRow, 1].Value = colSTT;
                var currentCol = 2;

                foreach (var column in columns)
                {
                    foreach (var property in properties)
                    {
                        if (column.ToLower() == property.Name.ToLower())
                        {
                            var cellValue = property.GetValue(item);
                            if (cellValue != null)
                            {
                                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                                {
                                    // Kiểm tra và định dạng nếu kiểu dữ liệu là DateTime hoặc DateTime?
                                    DateTime? nullableDateTime = cellValue as DateTime?;
                                    if (nullableDateTime.HasValue)
                                    {
                                        // Chuyển đổi giá trị thành DateTime trước khi định dạng
                                        DateTime actualDateTime = nullableDateTime.Value;
                                        cellValue = actualDateTime.ToString("dd/MM/yyyy HH:mm");
                                    }
                                    else
                                    {
                                        cellValue = ""; // Hoặc giá trị mặc định khác nếu thích
                                    }
                                }
                            }
                            if (cellValue != null)
                            {
                                if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
                                {
                                    // Kiểm tra nếu kiểu dữ liệu là double hoặc double?
                                    cellValue = ((double)cellValue).ToString("#,##0.00 đ"); // Hoặc cellValue = Convert.ToDouble(cellValue).ToString("#,##0.00 đ");
                                }
                            }

                            //gán giá trị cho từng cell                      
                            worksheet.Cells[currentRow, currentCol].Value = cellValue;
                            currentCol++;
                            break;
                        }
                    }
                }
                //set style cho từng ô giá trị
                worksheet.Cells[currentRow, 1, currentRow, totalCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells.AutoFitColumns();
                worksheet.Rows[currentRow].Height = 20;

                colSTT++;
                currentRow++;
            }
        }
    }
}

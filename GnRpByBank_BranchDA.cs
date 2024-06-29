using BO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class GnRpByBank_BranchDA
    {
        SalaryCedit salaryCedit = new SalaryCedit();
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();



        public List<string> BankBranches()
        {

            List<string> branchNames = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetBankBranchNames", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Bank_BrName = reader["Bank_BrName"].ToString();

                                branchNames.Add(Bank_BrName); // Add branch name to the list
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting branch names: " + ex.Message);
            }
            return branchNames;
        }

        //private string[] SplitLongName(string name)
        //{
        //    const int MaxLengthPerLine = 25;
        //    List<string> lines = new List<string>();

        //    while (name.Length > MaxLengthPerLine)
        //    {
        //        lines.Add(name.Substring(0, MaxLengthPerLine));
        //        name = name.Substring(MaxLengthPerLine);
        //    }

        //    lines.Add(name);

        //    return lines.ToArray();
        //}
        public List<string> GetBank_Name()
        {
            List<string> bankNames = new List<string>();
            //string Bank_Name = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetBank_Name", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string Bank_Name = reader["Bank_Name"].ToString();
                                bankNames.Add(Bank_Name);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting branch names: " + ex.Message);
            }
            return bankNames;
        }
        public Tuple<List<SalaryCedit>, string> GenerateEmployeeBankInfoReport(string selectedBankName, string selectedBranchName, DateTime nextProcessDate, string serverMapPath, int pageNumber)
        {
            List<SalaryCedit> employeeList = new List<SalaryCedit>();
            int serialNumber = 1;
            string formattedDate = nextProcessDate.ToString("MMMM yyyy");
            string currentTime = DateTime.Now.ToString("dd-MM-yyyy");

            Document document = new Document(PageSize.A4, 36, 36, 54, 54); // Added margins for better formatting
            string filePath = Path.Combine(serverMapPath, "App_Data", "Report");
            string reportFileName = Path.Combine(filePath, $"SalaryCreditReport_{currentTime}.pdf");

            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string logoPath = @"D:\HRMS-\logo.png"; // Provide the path to your logo image
                Image logoImage = Image.GetInstance(logoPath);
                logoImage.ScaleAbsolute(90, 50); // Set the size of the logo image

                // Initialize PDF writer
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportFileName, FileMode.Create));

                // Add header with logo
                PdfHeaderFooter header = new PdfHeaderFooter();
                header.Logo = logoImage; // Set the logo image to the header
                writer.PageEvent = header;

                document.Open();
                Font font = FontFactory.GetFont(FontFactory.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED, 8);

                // Add branch details
                document.Add(new Paragraph("\n", font));
                document.Add(new Paragraph("\n", font));
                document.Add(new Paragraph("\n", font));
                document.Add(new Paragraph("\n", font));
                document.Add(new Paragraph("\n", font));
                document.Add(new Paragraph($"BankName:{selectedBankName}                                                                       Page No :{pageNumber} ", font));
                document.Add(new Paragraph($"BranchName:{selectedBranchName}                                                       Date :{DateTime.Now.ToString("dd.MM.yyyy")} ", font));
                document.Add(new Paragraph("\n", font));

                // Create the table with 7 columns
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 5, 10, 25, 15, 15, 20, 10 });

                // Add header rows to the table
                table.AddCell(new PdfPCell(new Phrase("S.No", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("EmpNo", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("EmpName", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("BankName", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("BankBranch", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("AccountNo", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("NetSalary", font)) { HorizontalAlignment = Element.ALIGN_CENTER });

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();

                    using (SqlCommand employeeCommand = new SqlCommand("HRMS_BankCreditReportGeneration", connection))
                    {
                        employeeCommand.CommandType = CommandType.StoredProcedure;
                        employeeCommand.Parameters.AddWithValue("@Action", "S");
                        employeeCommand.Parameters.AddWithValue("@BankName", selectedBankName);
                        employeeCommand.Parameters.AddWithValue("@BankBranch", selectedBranchName);

                        using (SqlDataReader reader = employeeCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SalaryCedit salaryCedits = new SalaryCedit
                                {
                                    EmpNo = Convert.ToInt32(reader["Emp_No"]),
                                    EmpName = reader["Emp_Name"].ToString(),
                                    BankName = reader["Bank"].ToString(),
                                    BankBranch = reader["Branch"].ToString(),
                                    AccountNo = reader["Emp_AccNo"].ToString(),
                                    NetSalary = Convert.ToDecimal(reader["NetSalary"])
                                };
                                employeeList.Add(salaryCedits);
                            }
                        }
                    }

                    foreach (var employee in employeeList)
                    {
                        table.AddCell(new PdfPCell(new Phrase(serialNumber.ToString(), font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(employee.EmpNo.ToString(), font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(employee.EmpName, font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(employee.BankName, font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(employee.BankBranch, font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(employee.AccountNo, font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(employee.NetSalary.ToString("0.00"), font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        serialNumber++;
                    }

                    document.Add(table);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception("Access to the path is denied.", ex);
            }
            finally
            {
                // Close the document
                if (document != null)
                {
                    document.Close();
                }
            }

            return Tuple.Create(employeeList, reportFileName);
        }


        public DateTime nextProcessDate()
        {
            DateTime nextProcessDate = DateTime.MinValue; // Initialize to a default value
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetBranchLastProcessDates", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Assuming Dt is the column name for next process date
                                nextProcessDate = Convert.ToDateTime(reader["Dt"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting branch names: " + ex.Message);
            }
            return nextProcessDate; // Return the obtained next process date
        }
    }
    public class PdfHeaderFooter : PdfPageEventHelper
    {
        public Image Logo { get; set; } // Logo image property

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable headerTable = new PdfPTable(1);
            headerTable.TotalWidth = document.PageSize.Width;
            headerTable.DefaultCell.Border = 0;

            // Add logo to the header
            if (Logo != null)
            {
                PdfPCell cell = new PdfPCell(Logo);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                headerTable.AddCell(cell);
            }

            headerTable.WriteSelectedRows(0, -1, 0, document.Top + 10, writer.DirectContent);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tax_Calculator.Models;

namespace Tax_Calculator.Controllers
{
    public class HomeController : Controller
    {
        #region Connection String
        string connect = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\*\source\repos\Tax_Calculator\Tax_Calculator\App_Data\calculated_tax.mdf;Integrated Security = True";
        #endregion
        

        #region Table Values
        int value1 = 0;
        int value2 = 8351;
        int value3 = 33951;
        int value4 = 82251;
        int value5 = 171551;
        int value6 = 372950;
        #endregion

        #region Progressive Values
        decimal progressive_value1 = 0M;
        decimal progressive_value2 = 0M;
        decimal progressive_value3 = 0M;
        decimal progressive_value4 = 0M;
        decimal progressive_value5 = 0M;
        decimal progressive_taxed_value = 0M;
        #endregion

        #region Tax Percentages
        decimal tent_percent = 0.10M;
        decimal fifteen_percent = 0.15M;
        decimal twenty_five_percent = 0.25M;
        decimal twenty_eight_percent = 0.28M;
        decimal thirty_three_percent = 0.33M;
        decimal thirty_five_percent = 0.35M;
        #endregion

        #region Flat Rate Value
        decimal flat_rate_tax = 0.05M;
        decimal flat_value = 200000M;
        decimal non_tax = 10000M;
        #endregion

        #region String Values
        string error_message = "We do not recognice the entered prostal code. Please type one or the following: '7441', 'A100','7000','1000'";
        string progressive_tax_message = "Your progressive calculated tax is: ";
        #endregion


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Helpers helpers, string postal_code)
        {
            if (postal_code == "7441")
            {
                if (helpers.Annual_salary >= value1 && helpers.Annual_salary < value2)
                {
                    helpers.Tax_result = helpers.Annual_salary * 0.1M;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);

                }
                else if (helpers.Annual_salary >= value2 && helpers.Annual_salary < value3)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value2 - 1)) * fifteen_percent;

                    helpers.Tax_result = progressive_value1 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else if (helpers.Annual_salary >= value3 && helpers.Annual_salary < value4)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_value2 = (value3 - 1) * fifteen_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value3 - 1)) * twenty_five_percent;

                    helpers.Tax_result = progressive_value1 + progressive_value2 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else if (helpers.Annual_salary >= value4 && helpers.Annual_salary < value5)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_value2 = ((value3 - 1) ) * fifteen_percent;
                    progressive_value3 = (value4 - 1) * twenty_five_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value4 - 1)) * twenty_eight_percent;

                    helpers.Tax_result = progressive_value1 + progressive_value2 + progressive_value3 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else if (helpers.Annual_salary >= value5 && helpers.Annual_salary <= value6)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_value2 = (value3 - 1) * fifteen_percent;
                    progressive_value3 = (value4 - 1) * twenty_five_percent;
                    progressive_value4 = (value5 - 1) * twenty_eight_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value5 - 1)) * thirty_three_percent;

                    helpers.Tax_result = progressive_value1 + progressive_value2 + progressive_value3 + progressive_value4 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else if (helpers.Annual_salary > value6)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_value2 = (value3 - 1) * fifteen_percent;
                    progressive_value3 = (value4 -1) * twenty_five_percent;
                    progressive_value4 = (value5 - 1)  * twenty_eight_percent;
                    progressive_value5 = value6  * thirty_three_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value6 + 1)) * thirty_five_percent;

                    helpers.Tax_result = progressive_value1 + progressive_value2 + progressive_value3 + progressive_value4 + progressive_value5 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else
                {
                    helpers.Error_message = error_message;
                }
            }
            else if (postal_code == "A100")
            {
                if (helpers.Annual_salary < flat_value)
                {
                    helpers.Tax_result = helpers.Annual_salary * flat_rate_tax;
                    helpers.Tax_result_message = "Your Flat Value tax is: " + Math.Round(helpers.Tax_result) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else
                {
                    helpers.Tax_result = helpers.Annual_salary + non_tax;
                    helpers.Tax_result_message = "Your Flat Value for over 200000 is: " + helpers.Tax_result;

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
            }
            else if (postal_code == "7000")
            {
                helpers.Tax_result = helpers.Annual_salary * 0.175M;
                helpers.Tax_result_message = "Your Flate Rate calculated tax is: " + Math.Round(helpers.Tax_result, 2) + " annually.";

                InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
            }
            else if (postal_code == "1000")
            {
                if (helpers.Annual_salary >= value1 && helpers.Annual_salary < value2)
                {
                    helpers.Tax_result = helpers.Annual_salary * 0.1M;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);

                }
                else if (helpers.Annual_salary >= value2 && helpers.Annual_salary < value3)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value2 - 1)) * fifteen_percent;

                    helpers.Tax_result = progressive_value1 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else if (helpers.Annual_salary >= value3 && helpers.Annual_salary < value4)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_value2 = (value3 - 1) * fifteen_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value3 - 1)) * twenty_five_percent;

                    helpers.Tax_result = progressive_value1 + progressive_value2 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else if (helpers.Annual_salary >= value4 && helpers.Annual_salary < value5)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_value2 = ((value3 - 1)) * fifteen_percent;
                    progressive_value3 = (value4 - 1) * twenty_five_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value4 - 1)) * twenty_eight_percent;

                    helpers.Tax_result = progressive_value1 + progressive_value2 + progressive_value3 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else if (helpers.Annual_salary >= value5 && helpers.Annual_salary <= value6)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_value2 = (value3 - 1) * fifteen_percent;
                    progressive_value3 = (value4 - 1) * twenty_five_percent;
                    progressive_value4 = (value5 - 1) * twenty_eight_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value5 - 1)) * thirty_three_percent;

                    helpers.Tax_result = progressive_value1 + progressive_value2 + progressive_value3 + progressive_value4 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else if (helpers.Annual_salary > value6)
                {
                    progressive_value1 = (value2 - 1) * tent_percent;
                    progressive_value2 = (value3 - 1) * fifteen_percent;
                    progressive_value3 = (value4 - 1) * twenty_five_percent;
                    progressive_value4 = (value5 - 1) * twenty_eight_percent;
                    progressive_value5 = value6 * thirty_three_percent;
                    progressive_taxed_value = (helpers.Annual_salary - (value6 + 1)) * thirty_five_percent;

                    helpers.Tax_result = progressive_value1 + progressive_value2 + progressive_value3 + progressive_value4 + progressive_value5 + progressive_taxed_value;
                    helpers.Tax_result_message = progressive_tax_message + Math.Round(helpers.Tax_result, 2) + " annually.";

                    InsertIntoDB(helpers.Annual_salary, helpers.Postal_code, helpers.Tax_result, DateTime.Now);
                }
                else
                {
                    helpers.Error_message = error_message;
                }
            }
            else
            {
                helpers.Error_message = error_message;
            }
            return View(helpers);
        }

        public void InsertIntoDB(decimal annual_salary, string postal_code, decimal calculated_amount, DateTime date_time)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                sqlConnection.Open();

                using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                {
                    using (SqlCommand Command = sqlConnection.CreateCommand())
                    {
                        Command.CommandText = "INSERT INTO user_entries (annual_salary, postal_code, calculated_amount, date_calculated) " +
                                              "VALUES(@annual_salary, @postal_code, @calculated_amount, @date_time) ";

                        Command.Parameters.AddWithValue("annual_salary", annual_salary);
                        Command.Parameters.AddWithValue("postal_code", postal_code);
                        Command.Parameters.AddWithValue("calculated_amount", calculated_amount);
                        Command.Parameters.AddWithValue("date_time", date_time);

                        Command.Transaction = transaction;

                        if(Command.ExecuteNonQuery() == 1)
                        {
                            transaction.Commit();
                        }
                    }
                }
            }
        }
    }
}
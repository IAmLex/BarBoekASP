using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BarBoekASP.Data.MySQL;
using BarBoekASP.Data.Repositories;
using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MySql.Data.MySqlClient;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace BarBoekASP.Controllers
{
    public class GenerateReportController : Controller
    {
        iMemberRetrieveContext _iMemberRetrieveContext;
        MemberRetRepository memberRetRepository;
        public GenerateReportController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _iMemberRetrieveContext = new MemberMySQLContext(connectionString);
            memberRetRepository = new MemberRetRepository(_iMemberRetrieveContext);
        }
        public IActionResult Index(generateReportTotalListsViewModel viewmodel)
        {
            if (viewmodel.specifiersToAdd.Equals(null))
            {
                viewmodel = new generateReportTotalListsViewModel { };
                
            }
            fillTablesList();
            fillMembersList();

            return View(viewmodel);
        }

        public IActionResult ExecuteConstructedQuery(IConfiguration configuration,generateReportTotalListsViewModel viewmodel)
        {
            //ExecuteGivenQuery(configuration,viewmodel);

            return View(viewmodel);
        }

        [HttpPost]
        [Route("addspecifier")]
        public IActionResult addSpecifier(generateReportTotalListsViewModel reportInfo, string specifierRadios)
        {
            string rb = specifierRadios;
            if (specifierRadios == null)
            {
                rb = "none";
            }
            addThisSpecifier(reportInfo, rb);
            return RedirectToAction("Index", reportInfo);
        }


        [HttpPost]
        [Route("selectThisColumn")]
        public IActionResult SelectColumnsByTable(generateReportTotalListsViewModel reportInfo,string foo)
        {
            fillForSelectedTable(foo);
            reportInfo.columns = (List<string>)ViewData["columns"];
            reportInfo.selectedTable = foo;
            return RedirectToAction("Index", reportInfo);
        }

            private void fillMembersList()
        {
            List<MemberDTO> members = new List<MemberDTO> { };
            members = memberRetRepository.GetAll();
            ViewData["MemberList"] = members;
        }

        private void fillTablesList()
        {
            List<string> tables = new List<string> { "adres", "betaling", "certificaat", "certificaat-lid-combo", "dienst", "leden", "lid-dienst-combo", "nietbeschikbaar", "schema", "schema-dienst-combo", "vereniging" };
            ViewData["tables"] = tables;
        }

        private void fillForSelectedTable(string selected)
        {
            List<string> columns;

            switch (selected)
            {
                case "adres": { columns = fillForAdres(); break; };
                case "betaling": { columns = fillForBetaling(); break; };
                case "certificaat": { columns = fillForCertificaat(); break; };
                case "certificaat-lid-combo": { columns = fillForCertificaatLidCombo(); break; };
                case "dienst": { columns = fillForDienst(); break; };
                case "leden": { columns = fillForLeden(); break; };
                case "lid-dienst-combo": { columns = fillForLidDienstCombo(); break; };
                case "nietbeschikbaar": { columns = fillForNietBeschikbaar(); break; };
                case "schema": { columns = fillForSchema(); break; };
                case "schema-dienst-combo": { columns = fillForSchemaDienstCombo(); break; };
                case "vereniging": { columns = fillForVereniging(); break; };
                default: { columns = new List<string> { "Geen tabel geselecteerd" }; break; };
            }
            ViewData["columns"] = columns;
        }

        //ʕ•́ᴥ•̀ʔっ Fill for specific table ʕ•́ᴥ•̀ʔっ 
        private List<string> fillForAdres()
        {
            List<string> columns = new List<string> { "ID", "zipcode", "number", "addition" };
            return columns;
        }
        private List<string> fillForBetaling()
        {
            List<string> columns = new List<string> { "ID", "Lid_Dienst_ID", "geslaagd" };
            return columns;
        }
        private List<string> fillForCertificaat()
        {
            List<string> columns = new List<string> { "ID", "naam" };
            return columns;
        }
        private List<string> fillForCertificaatLidCombo()
        {
            List<string> columns = new List<string> { "ID", "certificaatID", "lidID", "behaaldOp", "verstrekingsDatum" };
            return columns;
        }
        private List<string> fillForDienst()
        {
            List<string> columns = new List<string> { "ID", "naam", "startMoment", "eindMoment", "soort", "maxLeden" };
            return columns;
        }
        private List<string> fillForLeden()
        {
            List<string> columns = new List<string> { "ID", "naam", "adresID", "geboortedatum", "email", "toegang", "bondsnummer", "achternaam", "voorletters", "tussenvoegsel", "telefoon", "geslacht", "telefoonWerk", "telefoonMobiel" };
            return columns;
        }
        private List<string> fillForLidDienstCombo()
        {
            List<string> columns = new List<string> { "ID", "lidID", "dienstID" };
            return columns;
        }
        private List<string> fillForNietBeschikbaar()
        {
            List<string> columns = new List<string> { "ID", "lidID", "beginMoment", "eindMoment" };
            return columns;
        }
        private List<string> fillForSchema()
        {
            List<string> columns = new List<string> { "ID", "naam", "verenigingID" };
            return columns;
        }
        private List<string> fillForSchemaDienstCombo()
        {
            List<string> columns = new List<string> { "ID", "dienstID", "schemaID" };
            return columns;
        }
        private List<string> fillForVereniging()
        {
            List<string> columns = new List<string> { "ID", "naam", "adresID", "email", "schemaID" };
            return columns;
        }

        private void addThisSpecifier(generateReportTotalListsViewModel viewmodel,string rbSelected)
        {
            switch (rbSelected)
            {
                case "name": viewmodel.NameSQLSpecifier = viewmodel.NameSQLSpecifier + addWhenNameRBSelected(viewmodel); break;
                case "date": viewmodel.ShiftDateSQLSpecifier = viewmodel.ShiftDateSQLSpecifier + addWhenDateRBSelected(viewmodel); break;
                case "age": viewmodel.AgeSQLSpecifier = viewmodel.AgeSQLSpecifier + addWhenAgeRBSelected(viewmodel); break;
                case "exceptionAbsence": viewmodel.AbsenceSQLSpecifier = viewmodel.AbsenceSQLSpecifier + addWhenExceptionRBSelected(viewmodel); break;
                case "group": viewmodel.GroupSQLSpecifier = viewmodel.GroupSQLSpecifier + addWhenGroupRBSelected(viewmodel); break;
                case "none": addWhenNoRBSelected(); break;
                default: addWhenNoRBSelected(); break;
            }
        }

        [HttpPost]
        public IActionResult addSelectedColumns(generateReportTotalListsViewModel viewmodel)
        {
            viewmodel.usedColumns = new List<string>();

            List<string> allChecked = new List<string> { "ID" };
            foreach (string column in allChecked)
            {
                string usedTable = viewmodel.selectedTable;
                string toUse = "`" + usedTable + "`." + column;
                if (!viewmodel.usedColumns.Contains(toUse))
                {
                    viewmodel.usedColumns.Add(toUse);
                }

            }
            if (viewmodel.selectedTable != null)
            {
                viewmodel.usedTables.Add(viewmodel.selectedTable);
            }
            return RedirectToAction("Index", viewmodel);
        }

        //ʕ•́ᴥ•̀ʔっ Add specific stuff ʕ•́ᴥ•̀ʔっ 
        private string addWhenNameRBSelected(generateReportTotalListsViewModel viewmodel)
        {
            string nameContains = "";
            if (viewmodel.specifiersToAdd.bevatTextInput != null)
            {
                nameContains = "((`leden`.naam LIKE '%" + viewmodel.specifiersToAdd.bevatTextInput + "%') OR (`leden`.achternaam LIKE '%" + viewmodel.specifiersToAdd.bevatTextInput + "%'))";
            }
            return nameContains;
        }
        private string addWhenDateRBSelected(generateReportTotalListsViewModel viewmodel)
        {
            DateTime dtFromInput = viewmodel.specifiersToAdd.vanafDateTimeInput;
            DateTime dtTillInput = viewmodel.specifiersToAdd.totDateTimeInput;
            string dateFilter = "";
            if (dtFromInput == null && dtTillInput != null)
            {
                dateFilter = "(`dienst`.eindMoment > " + dtTillInput.ToString() + ")";
            }
            else if (dtTillInput == null && dtFromInput != null)
            {
                dateFilter = "(`dienst`.startMoment < " + dtTillInput.ToString() + ")";
            }
            else if (dtFromInput < dtTillInput)
            {
                dateFilter = "((`dienst`.startMoment > " + dtFromInput.ToString() + ") AND (`dienst`.eindMoment < " + dtTillInput.ToString() + "))";
            }
            else if (dtFromInput > dtTillInput)
            {
                dateFilter = "((`dienst`.startMoment > " + dtFromInput.ToString() + ") OR (`dienst`.eindMoment < " + dtTillInput.ToString() + "))";
            }
            else if (dtFromInput == null && dtTillInput == null)
            {
                dateFilter = "";
            }
            return dateFilter;


        }
        private string addWhenAgeRBSelected(generateReportTotalListsViewModel viewmodel)
        {
            string fromInput = viewmodel.specifiersToAdd.vanafTextInput;
            string tillInput = viewmodel.specifiersToAdd.totTextInput;
            string ageFilter = "";
            bool successFrom = int.TryParse(fromInput, out int fromAge);
            bool successTill = int.TryParse(tillInput, out int tillAge);
            DateTime Today = DateTime.Now;
            DateTime BirthdayFrom;
            DateTime BirthdayTill;
            if (successFrom == false && successTill == true)
            {
                BirthdayTill = Today.AddYears((tillAge) * -1);
                ageFilter = "(`leden`.geboortedatum < " + BirthdayTill.ToString() + ")";
            }
            else if (successTill == false && successFrom == true)
            {
                BirthdayFrom = Today.AddYears((fromAge + 1) * -1);
                ageFilter = "(`leden`.geboortedatum >  " + BirthdayFrom.ToString() + ")";
            }
            else if (successFrom == false && successTill == false)
            {
                ageFilter = "";

            }
            else if (fromAge > tillAge)
            {
                BirthdayFrom = Today.AddYears((fromAge + 1) * -1);
                BirthdayTill = Today.AddYears((tillAge) * -1);
                ageFilter = "((`leden`.geboortedatum < " + BirthdayTill.ToString() + ") OR (`leden`.geboortedatum >  " + BirthdayFrom.ToString() + "))";
            }
            else if (fromAge < tillAge)
            {
                BirthdayFrom = Today.AddYears((fromAge + 1) * -1);
                BirthdayTill = Today.AddYears((tillAge) * -1);
                ageFilter = "((`leden`.geboortedatum < " + BirthdayTill.ToString() + ") AND (`leden`.geboortedatum >  " + BirthdayFrom.ToString() + "))";
            }
            else if (fromAge == tillAge)
            {
                BirthdayFrom = Today.AddYears((fromAge + 1) * -1);
                BirthdayTill = Today.AddYears((tillAge) * -1);
                ageFilter = "((`leden`.geboortedatum < " + BirthdayTill.ToString() + ") AND (`leden`.geboortedatum >  " + BirthdayFrom.ToString() + "))";
            }
            return ageFilter;

        }
        private string addWhenExceptionRBSelected(generateReportTotalListsViewModel viewmodel)
        {

            DateTime dtFromInput = viewmodel.specifiersToAdd.vanafDateTimeInput;
            DateTime dtTillInput = viewmodel.specifiersToAdd.totDateTimeInput;
            string dateFilter = "";
            //¯\_(ツ)_/¯ Add Absence to Database / Class Diagram ¯\_(ツ)_/¯
            if (dtFromInput == null && dtTillInput != null)
            {
                /*dateFilter = "(`dienst`.eindMoment > " + dtTillInput.ToString() + ")";*/
            }
            else if (dtTillInput == null && dtFromInput != null)
            {
                //dateFilter = "(`dienst`.startMoment < " + dtTillInput.ToString() + ")";
            }
            else if (dtFromInput < dtTillInput)
            {
                //dateFilter = "((`dienst`.startMoment > " + dtFromInput.ToString() + ") AND (`dienst`.eindMoment < " + dtTillInput.ToString() + "))";
            }
            else if (dtFromInput > dtTillInput)
            {
                //dateFilter = "((`dienst`.startMoment > " + dtFromInput.ToString() + ") OR (`dienst`.eindMoment < " + dtTillInput.ToString() + "))";
            }
            else if (dtFromInput == null && dtTillInput == null)
            {
                //dateFilter = "";
            }
            return dateFilter;
        }
        private string addWhenGroupRBSelected(generateReportTotalListsViewModel viewmodel)
        {
            string groupContains = "";
            if (viewmodel.specifiersToAdd.bevatTextInput != null)
            {
                //¯\_(ツ)_/¯ ADD GROUPS TO DATABASE + CLASS DIAGRAM ¯\_(ツ)_/¯
                //groupContains = "((`leden`.naam LIKE '%" + TBContains.Text + "%') OR (`leden`.achternaam LIKE '%" + TBContains.Text + "%'))";
            }
            return groupContains;
        }
        private void addWhenNoRBSelected()
        {
            //Message.Show("Er is geen RadioButton geselecteerd.");
        }


        public string composeQuery(generateReportTotalListsViewModel viewmodel)
        {
            string composedQuery = "SELECT " + usedColumnsString(viewmodel) + " FROM " + usedTablesString(viewmodel);
            if (stringHasValue(combinedSpecificationsString(viewmodel)))
            {
                composedQuery = composedQuery + " WHERE " + combinedSpecificationsString(viewmodel);
            }
            return composedQuery;
        }

        public string usedTablesString(generateReportTotalListsViewModel viewmodel)
        {
            string usedTablesString = "";
            int count = viewmodel.usedTables.Count;
            int tablesUsed = 1;
            foreach (string table in viewmodel.usedTables)
            {
                string toUse = "`" + table + "`";
                usedTablesString = usedTablesString + leftJoinString(viewmodel, toUse);

                if (tablesUsed < count)
                {
                    usedTablesString = usedTablesString + ", ";
                }
                tablesUsed++;
            }
            return usedTablesString;
        }

        public string usedColumnsString(generateReportTotalListsViewModel viewmodel)
        {
            string usedColumnsString = "";
            int count = viewmodel.usedColumns.Count;
            int columnsUsed = 1;
            foreach (string column in viewmodel.usedColumns)
            {
                usedColumnsString = usedColumnsString + "" + column + "";
                if (columnsUsed < count)
                {
                    usedColumnsString = usedColumnsString + ", ";
                }
                columnsUsed++;
            }
            return usedColumnsString;
        }

        public string combinedSpecificationsString(generateReportTotalListsViewModel viewmodel)
        {
            string combinedSpecificationsString = "";

            combinedSpecificationsString = combinedSpecificationsString + viewmodel.NameSQLSpecifier;
            combinedSpecificationsString = addAndBetweenStringsWhenLastStringIsNotEmpty(combinedSpecificationsString, viewmodel.ShiftDateSQLSpecifier);
            combinedSpecificationsString = addAndBetweenStringsWhenLastStringIsNotEmpty(combinedSpecificationsString, viewmodel.AgeSQLSpecifier);
            combinedSpecificationsString = addAndBetweenStringsWhenLastStringIsNotEmpty(combinedSpecificationsString, viewmodel.AbsenceSQLSpecifier);
            combinedSpecificationsString = addAndBetweenStringsWhenLastStringIsNotEmpty(combinedSpecificationsString, viewmodel.GroupSQLSpecifier);

            return combinedSpecificationsString;
        }

        public bool stringHasValue(string toCheck)
        {
            bool result = false;
            if (toCheck.Length > 0)
            {
                result = true;
            }
            return result;
        }

        public string addAndBetweenStringsWhenLastStringIsNotEmpty(string first, string last)
        {

            string result = "";
            if (stringHasValue(last))
            {
                result = first + " AND " + last;
            }
            else
            {
                result = first + "" + last;
            }
            return result;
        }

        public string leftJoinString(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "";
            switch (table)
            {
                case "`betaling`": leftJoinString = leftJoinStringBetaling(viewmodel,table); break;
                case "`certificaat-lid-combo`": leftJoinString = leftJoinStringCertificaatLidCombo(viewmodel, table); break;
                case "`leden`": leftJoinString = leftJoinStringLeden(viewmodel, table); break;
                case "`lid-dienst-combo`": leftJoinString = leftJoinStringLidDienstCombo(viewmodel, table); break;
                case "`nietbeschikbaar`": leftJoinString = leftJoinStringNietBeschikbaar(viewmodel, table); break;
                case "`schema`": leftJoinString = leftJoinStringSchema(viewmodel, table); break;
                case "`schema-dienst-combo`": leftJoinString = leftJoinStringSchemaDienstCombo(viewmodel, table); break;
                case "`vereniging`": leftJoinString = leftJoinStringVereniging(viewmodel, table); break;
                default: leftJoinString = "(" + table + ")"; break;
            }
            return leftJoinString;
        }
        public string leftJoinStringBetaling(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "(" + table;
            if (viewmodel.usedTables.Contains("leden") || viewmodel.usedTables.Contains("dienst") || viewmodel.usedTables.Contains("lid-dienst-combo"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN `lid-dienst-combo` ldc_b ON ldc_b.ID = betaling.Lid_Dienst_ID";
                if (viewmodel.usedTables.Contains("leden"))
                {
                    leftJoinString = leftJoinString + "LEFT JOIN leden l_b ON l_b.ID = ldc_b.lidID";
                }
                if (viewmodel.usedTables.Contains("dienst"))
                {
                    leftJoinString = leftJoinString + "LEFT JOIN dienst d_b ON d_b.ID = ldc_b.dienstID";
                }
            }
            leftJoinString = leftJoinString + ")";

            return leftJoinString;
        }
        public string leftJoinStringCertificaatLidCombo(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "(" + table;
            if (viewmodel.usedTables.Contains("leden"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN leden l_clb ON l_clb.ID = `certificaat-lid-combo`.lidID";
            }
            if (viewmodel.usedTables.Contains("leden"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN certificaat c_clc ON c_clc.ID = `certificaat-lid-combo`.certificaatID";
            }
            leftJoinString = leftJoinString + ")";

            return leftJoinString;
        }
        public string leftJoinStringLeden(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "(" + table;
            if (viewmodel.usedTables.Contains("adres"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN adres a_l ON a_l.ID = leden.adresID";
            }
            if (viewmodel.usedTables.Contains("vereniging"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN vereniging v_l ON v_l.ID = leden.bondsnummer";
            }
            leftJoinString = leftJoinString + ")";

            return leftJoinString;
        }
        public string leftJoinStringLidDienstCombo(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "(" + table;
            if (viewmodel.usedTables.Contains("leden"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN leden l_ldc ON l_ldc.ID = `lid-dienst-combo`.lidID";
            }
            if (viewmodel.usedTables.Contains("dienst"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN dienst d_ldc ON d_ldc.ID = `lid-dienst-combo`.dienstID";
            }
            leftJoinString = leftJoinString + ")";

            return leftJoinString;
        }
        public string leftJoinStringNietBeschikbaar(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "(" + table;

            if (viewmodel.usedTables.Contains("leden"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN leden l_nb ON l_nb.ID = nietbeschikbaar.lidID";
            }
            leftJoinString = leftJoinString + ")";

            return leftJoinString;
        }
        public string leftJoinStringSchema(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "(" + table;

            if (viewmodel.usedTables.Contains("vereniging"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN vereniging v_s ON v_s.ID = `schema`.verenigingID";
            }
            leftJoinString = leftJoinString + ")";

            return leftJoinString;
        }
        public string leftJoinStringSchemaDienstCombo(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "(" + table;
            if (viewmodel.usedTables.Contains("schema"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN leden s_sdc ON s_sdc.ID = `schema-dienst-combo`.schemaID";
            }
            if (viewmodel.usedTables.Contains("dienst"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN dienst s_sdc ON s_sdc.ID = `schema-dienst-combo`.dienstID";
            }
            leftJoinString = leftJoinString + ")";

            return leftJoinString;
        }
        public string leftJoinStringVereniging(generateReportTotalListsViewModel viewmodel,string table)
        {
            string leftJoinString = "(" + table;
            if (viewmodel.usedTables.Contains("adres"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN adres a_v ON a_v.ID = `vereniging`.adresID";
            }
            if (viewmodel.usedTables.Contains("schema"))
            {
                leftJoinString = leftJoinString + "LEFT JOIN schema a_v ON a_v.ID = `vereniging`.schemaID";
            }
            leftJoinString = leftJoinString + ")";

            return leftJoinString;
        }

        private bool ExecuteGivenQuery(IConfiguration configuration, generateReportTotalListsViewModel viewmodel)
        {
            try
            {
                string query = composeQuery(viewmodel);
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                DataSet results = ExecuteQuery(configuration, query, parameters);
                //dgvShowResults.AutoGenerateColumns = true;
                //dgvShowResults.DataSource = results; // dataset
                if (results != null)
                {
                    foreach (DataTable table in results.Tables)
                    {
                        //dgvShowResults.DataSource = table;
                    }
                }
                else
                {
                    // MessageBox.Show("Er ging iets fout bij het opvragen van de gegevens. Mocht u apart alle gegevens van een tabel willen opvragen, zonder relatie, probeer dit dan stuk voor stuk");
                }

                //dgvShowResults.DataMember = "Results";

                return true;
            }
            catch (Exception)
            {
                return false;
            }
                
        }

        public DataSet ExecuteQuery(IConfiguration configuration, string query, List<KeyValuePair<string, string>> parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand cmd = conn.CreateCommand();
                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    MySqlParameter para = new MySqlParameter();
                    para.ParameterName = "@" + kvp.Key;
                    para.Value = "@" + kvp.Value;
                    cmd.Parameters.Add(para);
                }
                cmd.CommandText = query;
                da.SelectCommand = cmd;

                conn.Open();
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                
                return null;
            }
            return ds;
        }

        

        

       
        
    }
}
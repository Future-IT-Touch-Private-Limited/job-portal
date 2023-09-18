using System;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.Hosting;

public partial class excel_upload : System.Web.UI.Page
{
    string EncryptionKey = "ravindersinghgodara123admin";
    string EncryptionKey2 = "rvndr@123@adm";
    public static string MD5Hash(string text)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
        byte[] result = md5.Hash;
        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            strBuilder.Append(result[i].ToString("x2"));
        }
        return strBuilder.ToString();
    }
    public string EncryptString(string Message, string Passphrase)
    {
        byte[] Results;
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;
        byte[] DataToEncrypt = UTF8.GetBytes(Message);
        try
        {
            ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
        }
        finally
        {
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }
        return Convert.ToBase64String(Results);
    }

    public string DecryptString(string Message, string Passphrase)
    {
        byte[] Results;
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;
        byte[] DataToDecrypt = Convert.FromBase64String(Message);
        try
        {
            ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
        }
        finally
        {
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }
        return UTF8.GetString(Results);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    SqlCommand com;
    protected void uploadbtn_Click(object sender, EventArgs e)
    {

        try
        {
            errorpanel.Visible = false;
            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
            }
        }
        catch (Exception ex)
        {
            errorpanel.Visible = true;
            errorlbl.Text = ex.Message;
            return;
        }
    }

    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        //Bind Data to GridView
        GridView1.Caption = Path.GetFileName(FilePath);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        uploadjobsbtn.Visible = true;
    }

    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
        string FileName = GridView1.Caption;
        string Extension = Path.GetExtension(FileName);
        string FilePath = Server.MapPath(FolderPath + FileName);

        Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }


    private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


    protected void uploadjobsbtn_Click(object sender, EventArgs e)
    {
        //try
        //{
        errorpanel.Visible = false;
        logsliteral.Text = "";

        logsliteral.Text = "<table class='table table-bordered table-hover table-stripped'>";
        foreach (GridViewRow g1 in GridView1.Rows)
        {
            string str_state = "";
            string str_district = "";
            string str_area = "";
            string str_pin_code = "";
            string str_qualification = "";
            string str_no_of_posts = "";
            string str_valid_through = "";
            string str_responsibilities = "";
            string str_salery = "";
            //check state
            if (g1.Cells[0].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[0].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_state = "Haryana";
            }
            else
            {
                str_state = g1.Cells[0].Text.Trim().ToString();
            }

            //check district
            if (g1.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("&nbsp;", "").ToString() == "" || g1.Cells[1].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_district = str_state;
            }
            else
            {
                str_district = g1.Cells[1].Text.Trim().ToString();
            }

            //check area
            if (g1.Cells[2].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[2].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_area = str_state;
            }
            else
            {
                str_area = g1.Cells[2].Text.Trim().ToString();
            }

            //check pincode
            if (g1.Cells[3].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[3].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_pin_code = str_state;
            }
            else
            {
                str_pin_code = g1.Cells[3].Text.Trim().ToString();
            }

            //check qualification
            if (g1.Cells[8].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[8].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_qualification = "Degree";
            }
            else
            {
                str_qualification = g1.Cells[8].Text.Trim().ToString();
            }

            //check no. of posts
            if (g1.Cells[7].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[7].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_no_of_posts = "Update Soon";
            }
            else
            {
                str_no_of_posts = g1.Cells[7].Text.Trim().ToString();
            }

            //check valid through
            if (g1.Cells[9].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[9].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_valid_through = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
            }
            else
            {
                str_valid_through = g1.Cells[9].Text.Trim().ToString();
            }

            //check valid through
            if (g1.Cells[6].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[6].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_responsibilities = g1.Cells[6].Text.Trim().ToString();
            }
            else
            {
                str_responsibilities = g1.Cells[6].Text.Trim().ToString();
            }

            //check responsibilities
            if (g1.Cells[6].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[6].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_responsibilities = g1.Cells[6].Text.Trim().ToString();
            }
            else
            {
                str_responsibilities = g1.Cells[6].Text.Trim().ToString();
            }
            //check salery
            if (g1.Cells[12].Text.Replace("&nbsp;", "").Trim().ToString() == "" || g1.Cells[12].Text.Replace("&nbsp;", "").Trim().ToString() == null)
            {
                str_salery = "35000";
            }
            else
            {
                str_salery = g1.Cells[12].Text.Trim().ToString();
            }


            if (g1.Cells[0].Text.Trim() != "&nbsp;" || g1.Cells[6].Text.Trim() != "&nbsp;")
            {
                SqlConnection con2 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                con2.Open();
                SqlCommand cmdd2 = new SqlCommand("select * from job_site_posts where sch_industry=@sch_industry and sch_post_name=@sch_post_name and sch_number_of_posts=@sch_number_of_posts and pdf_url=@pdf_url", con2);
                cmdd2.Parameters.AddWithValue("@sch_industry", g1.Cells[5].Text.Trim().ToString());
                cmdd2.Parameters.AddWithValue("@sch_post_name", g1.Cells[6].Text.Trim().ToString());
                cmdd2.Parameters.AddWithValue("@sch_number_of_posts", g1.Cells[7].Text.Trim().ToString());
                cmdd2.Parameters.AddWithValue("@pdf_url", g1.Cells[10].Text.Trim().ToString());
                SqlDataReader reader = cmdd2.ExecuteReader();
                if (reader.HasRows)
                {
                    logsliteral.Text += "<tr>";
                    logsliteral.Text += "<td>";
                    logsliteral.Text += "<b>Industry: </b>" + g1.Cells[5].Text.Trim().ToString() + " <br><b>Post Name:</b>" + g1.Cells[6].Text.Trim().ToString() + " <br><b>No. of Posts: </b>" + g1.Cells[7].Text.Trim().ToString() + "";
                    logsliteral.Text += "</td>";
                    logsliteral.Text += "<td>";
                    logsliteral.Text += "<span style='color:red'><b>Already Exist</b></span>";
                    logsliteral.Text += "</td>";
                    logsliteral.Text += "</tr>";
                }
                else
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                        com = new SqlCommand("Insert into job_site_posts (sch_industry,query_sch_industry,sch_organization_name,query_sch_organization_name,sch_post_name,query_sch_post_name,state,query_state,district,query_district,area,query_area,pincode,sch_number_of_posts,main_category,query_main_category,sub_category,query_sub_category,sch_qualification,query_sch_qualification,sch_education_requirements,query_sch_education_requirements,sch_valid_through,sch_last_date,pdf_url,sch_apply_now_url,sch_responsibilities,sch_skills,sch_salery,sch_base_salery,sch_min_salery,sch_max_salery,sch_employment_type,sch_working_hours,sch_street_address,sch_locality,sch_region,sch_pay_scale,sch_experience_requirements,thumbnail_url,tags,query_tags,page_short_url,page_full_url,last_update,post_published,post_status,schema_status,query_valid_through,submitted_username,submitted_date) values (@sch_industry,@query_sch_industry,@sch_organization_name,@query_sch_organization_name,@sch_post_name,@query_sch_post_name,@state,@query_state,@district,@query_district,@area,@query_area,@pincode,@sch_number_of_posts,@main_category,@query_main_category,@sub_category,@query_sub_category,@sch_qualification,@query_sch_qualification,@sch_education_requirements,@query_sch_education_requirements,@sch_valid_through,@sch_last_date,@pdf_url,@sch_apply_now_url,@sch_responsibilities,@sch_skills,@sch_salery,@sch_base_salery,@sch_min_salery,@sch_max_salery,@sch_employment_type,@sch_working_hours,@sch_street_address,@sch_locality,@sch_region,@sch_pay_scale,@sch_experience_requirements,@thumbnail_url,@tags,@query_tags,@page_short_url,@page_full_url,@last_update,@post_published,@post_status,@schema_status,@query_valid_through,@submitted_username,@submitted_date)", con);

                        com.Parameters.AddWithValue("@sch_industry", g1.Cells[5].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@query_sch_industry", g1.Cells[5].Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
                        com.Parameters.AddWithValue("@sch_organization_name", g1.Cells[5].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@query_sch_organization_name", g1.Cells[5].Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
                        com.Parameters.AddWithValue("@sch_post_name", g1.Cells[6].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@query_sch_post_name", g1.Cells[6].Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace(",", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace("--", "-").Replace("--", "-"));
                        com.Parameters.AddWithValue("@state", str_state.Trim().ToString());
                        com.Parameters.AddWithValue("@query_state", str_state.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
                        com.Parameters.AddWithValue("@district", str_district.Trim().ToString());
                        com.Parameters.AddWithValue("@query_district", str_district.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
                        com.Parameters.AddWithValue("@area", str_area.Trim().ToString());
                        com.Parameters.AddWithValue("@query_area", str_area.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
                        com.Parameters.AddWithValue("@pincode", str_pin_code.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_number_of_posts", str_no_of_posts.Trim().ToString());
                        com.Parameters.AddWithValue("@main_category", g1.Cells[4].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@query_main_category", g1.Cells[4].Text.Trim().ToLower().Replace(" ", "-"));
                        com.Parameters.AddWithValue("@sub_category", g1.Cells[6].Text.Trim().ToString() + " Jobs");
                        com.Parameters.AddWithValue("@query_sub_category", (g1.Cells[6].Text.Trim().ToLower() + " jobs").Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace("/", ","));
                        com.Parameters.AddWithValue("@sch_qualification", str_qualification.Trim().ToString().Replace("/", ","));
                        com.Parameters.AddWithValue("@query_sch_qualification", str_qualification.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace(".", "-").Replace("--", "-"));
                        com.Parameters.AddWithValue("@sch_education_requirements", g1.Cells[8].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@query_sch_education_requirements", g1.Cells[8].Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace(".", "-").Replace("--", "-"));
                        com.Parameters.AddWithValue("@sch_valid_through", str_valid_through.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_last_date", g1.Cells[9].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@pdf_url", g1.Cells[10].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_apply_now_url", g1.Cells[13].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_responsibilities", str_responsibilities.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_skills", g1.Cells[11].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_salery", g1.Cells[12].Text.Trim().ToString());
                        //com.Parameters.AddWithValue("@sch_base_salery", g1.Cells[12].Text.Trim().ToString());
                        int int_sch_min_salery = 0;
                        int int_sch_base_salery = 0;
                        int int_sch_max_salery = 0;
                        try
                        {
                            if (str_salery.Trim() == "" || str_salery.Trim() == null)
                            {
                                int_sch_min_salery = Convert.ToInt32(str_salery.Trim()) - 5000;
                                int_sch_base_salery = Convert.ToInt32(str_salery.Trim().ToString());
                                int_sch_max_salery = Convert.ToInt32(str_salery.Trim()) + 5000;
                            }
                        }
                        catch (Exception ex)
                        {
                            int_sch_min_salery = 0;
                            int_sch_base_salery = 0;
                            int_sch_max_salery = 0;
                        }
                        com.Parameters.AddWithValue("@sch_min_salery", int_sch_min_salery.ToString());
                        com.Parameters.AddWithValue("@sch_base_salery", int_sch_base_salery.ToString());
                        com.Parameters.AddWithValue("@sch_max_salery", int_sch_max_salery.ToString());
                        com.Parameters.AddWithValue("@sch_employment_type", "FULL_TIME");
                        com.Parameters.AddWithValue("@sch_working_hours", "9AM to 5PM");
                        com.Parameters.AddWithValue("@sch_street_address", g1.Cells[2].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_locality", g1.Cells[1].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_region", g1.Cells[0].Text.Trim().ToString());
                        com.Parameters.AddWithValue("@sch_pay_scale", "1200");
                        com.Parameters.AddWithValue("@sch_experience_requirements", "Freshers and Experienced");
                        com.Parameters.AddWithValue("@thumbnail_url", "");
                        com.Parameters.AddWithValue("@tags", g1.Cells[5].Text.Trim() + " Jobs," + g1.Cells[6].Text.Trim() + " Requirements, " + g1.Cells[1].Text.ToString() + " jobs");
                        com.Parameters.AddWithValue("@query_tags", (g1.Cells[5].Text.Trim() + " Jobs," + g1.Cells[6].Text.Trim() + " Requirements").ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
                        com.Parameters.AddWithValue("@page_short_url", "");
                        com.Parameters.AddWithValue("@page_full_url", "");
                        com.Parameters.AddWithValue("@last_update", indianTime.ToString("yyyy-MM-ddTHH:mm:ss"));
                        com.Parameters.AddWithValue("@post_published", indianTime.ToString("yyyy-MM-ddTHH:mm:ss"));
                        com.Parameters.AddWithValue("@post_status", "Submitted");
                        com.Parameters.AddWithValue("@schema_status", "Applied");
                        com.Parameters.AddWithValue("@query_valid_through", g1.Cells[8].Text.Trim().ToString().Replace("-", ""));
                        com.Parameters.AddWithValue("@submitted_username", "31xQpiDt1ABgUgqQxaC4oQ==");
                        com.Parameters.AddWithValue("@submitted_date", indianTime.ToString("dd-MM-yyyy"));
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                        logsliteral.Text += "<tr>";
                        logsliteral.Text += "<td>";
                        logsliteral.Text += "<b>Industry: </b>" + g1.Cells[5].Text.Trim().ToString() + " <br><b>Post Name:</b>" + g1.Cells[6].Text.Trim().ToString() + " <br><b>No. of Posts: </b>" + g1.Cells[7].Text.Trim().ToString() + "";
                        logsliteral.Text += "</td>";
                        logsliteral.Text += "<td>";
                        logsliteral.Text += "<span style='color:green'><b>Job Uploaded</b></span>";
                        logsliteral.Text += "</td>";
                        logsliteral.Text += "</tr>";
                    }
                    catch (Exception ex)
                    {
                        logsliteral.Text += "<tr>";
                        logsliteral.Text += "<td>";
                        logsliteral.Text += "<b>Industry: </b>" + g1.Cells[5].Text.Trim().ToString() + " <br><b>Post Name:</b>" + g1.Cells[6].Text.Trim().ToString() + " <br><b>No. of Posts: </b>" + g1.Cells[7].Text.Trim().ToString() + "";
                        logsliteral.Text += "</td>";
                        logsliteral.Text += "<td>";
                        logsliteral.Text += "<span style='color:blue'><b>" + ex.Message + "</b></span>";
                        logsliteral.Text += "</td>";
                        logsliteral.Text += "</tr>";

                    }
                }
                con2.Close();
                reader.Close();
            }
            else
            {
                logsliteral.Text += "<tr>";
                logsliteral.Text += "<td>";
                logsliteral.Text += "<b>Industry: </b> <br><b>Post Name:</b> <br><b>No. of Posts: </b>";
                logsliteral.Text += "</td>";
                logsliteral.Text += "<td>";
                logsliteral.Text += "<span style='color:blue'><b>Data Not Found</b></span>";
                logsliteral.Text += "</td>";
                logsliteral.Text += "</tr>";
            }
            //}
            //    catch (Exception ex)
            //{
            //    errorpanel.Visible = true;
            //    errorlbl.Text += ex.Message + "<hr>";
            //    return;
            //}
        }
                logsliteral.Text += "</table>";
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            if (e.Row.Cells[1].Text.Trim() == "&nbsp;")
                e.Row.Visible = false;
    }
}
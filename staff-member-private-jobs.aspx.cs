using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration.Provider;
using System.Net;
using System.Collections;
using System.Web.Configuration;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Security.Cryptography;

public partial class adm_staff_member_private_jobs : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    int inc = 0;
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


    PagedDataSource pgsource = new PagedDataSource();
    int findex, lindex;
    DataRow dr;
    DataTable GetData()
    {
        DataTable dtable = new DataTable();
        SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
        string strcon = "select * from job_site_posts where post_status=@post_status and main_category=@main_category ORDER BY sr DESC";
        cmd = new SqlCommand(strcon, con);
        cmd.Parameters.AddWithValue("@post_status", "Submitted");
        cmd.Parameters.AddWithValue("@main_category", "Private Jobs");
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(dtable);
        con.Close();
        con.Dispose();
        return dtable;
    }

    private void BindDataList()
    {
        try
        {
            DataTable dt = GetData();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = CurrentPage;
            ViewState["totpage"] = pgsource.PageCount;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + pgsource.PageCount;
            lnkPrevious.Enabled = !pgsource.IsFirstPage;
            lnkNext.Enabled = !pgsource.IsLastPage;
            lnkFirst.Enabled = !pgsource.IsFirstPage;
            lnkLast.Enabled = !pgsource.IsLastPage;
            Repeater1.DataSource = pgsource;
            Repeater1.DataBind();
            doPaging();
            RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        }
        catch (Exception ex)
        {

        }
    }

    private void doPaging()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            findex = CurrentPage - 5;
            if (CurrentPage > 5)
            {
                lindex = CurrentPage + 5;
            }
            else
            {
                lindex = 10;
            }
            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }
            if (findex < 0)
            {
                findex = 0;
            }
            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }
            RepeaterPaging.DataSource = dt;
            RepeaterPaging.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    private int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] == null)
            {
                return 0;
            }
            else
            {
                return ((int)ViewState["CurrentPage"]);
            }
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }
    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentPage = 0;
            BindDataList();
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkLast_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindDataList();
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentPage -= 1;
            BindDataList();
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentPage += 1;
            BindDataList();
        }
        catch (Exception ex)
        {
        }
    }
    protected void RepeaterPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("newpage"))
            {
                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                BindDataList();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void RepeaterPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn");
            if (lnkPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#FFCC01");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                BindDataList();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label sr_lbl = (Label)e.Item.FindControl("submitted_by_lbl");
        string str_sr_lbl = sr_lbl.Text;
        sr_lbl.Text = DecryptString(sr_lbl.Text.ToString(), EncryptionKey);
    }

    protected void deleteitem_Click(object sender, EventArgs e)
    {
        sucesspanel.Visible = false;
        errorpanel.Visible = false;
        foreach (RepeaterItem Item in Repeater1.Items)
        {
            CheckBox chkDelete = (CheckBox)Item.FindControl("chkDelete");
            HiddenField hdnID = (HiddenField)Item.FindControl("hdnid");
            string hdnID_value = hdnID.Value;
            if (chkDelete.Checked)
            {
                //delete data
                DataTable dt4 = new DataTable();
                SqlConnection con4 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                con4.Open();
                string strcon4 = "delete from job_site_posts where sr=@sr";
                SqlCommand cmd4 = new SqlCommand(strcon4, con4);
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                cmd4.Parameters.AddWithValue("@sr", hdnID_value);
                cmd4.ExecuteNonQuery();
                con4.Close();
                con4.Dispose();
                sucesspanel.Visible = true;
                sucesslbl.Text = "Data Deleted Sucessfully!";
            }
        }
        try
        {
            BindDataList();
        }
        catch (Exception ex)
        {
        }
    }


}
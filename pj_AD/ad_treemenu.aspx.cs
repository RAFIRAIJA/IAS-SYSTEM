using System.IO;
using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.SessionState;
using System.Xml.Linq;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit; 
using Microsoft.VisualBasic;
using IASClass;
using IAS.Core.CSCode;

public partial class pj_ad_ad_treemenu : System.Web.UI.Page
{    
    
    ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    ucmImageSystem mlOBJIS = new ucmImageSystem();

    /* --new Class Core-- */
    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    

    protected void Page_Load(object sender, EventArgs e)
    {

        string XMLFileName;
        string AppId = "";
        string Style = "";
        string Password = "";

        bool LNumValid = true;
        Response.Flush();        
        AppId = "ISS";
        
        Style = "AccMnt"; 
        oMDBF.Loginid = Session["mgUSERID"].ToString();
        
        XMLFileName = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "XML\\" + "Menu_" + AppId + "_" + oMDBF.Loginid + ".xml";

        if (!File.Exists(XMLFileName))
        {
            oMDBF.WriteXMLNew(AppId, oMDBF.Loginid, XMLFileName);
        }
        else
        {
            File.Delete(XMLFileName);
            oMDBF.WriteXMLNew(AppId, oMDBF.Loginid, XMLFileName);
        }

        //MySource.DataFile = XMLFileName;

        DataSet dsMenu = new DataSet();
        dsMenu.ReadXml(XMLFileName);
        DataTable dtMenu = new DataTable();
        dtMenu = dsMenu.Tables[0].Copy();
        if (dtMenu.Rows.Count > 0)
        {
            AccordionPane pn;

            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                if (dtMenu.Rows[i]["MsMenuItem_Id_0"].ToString() == "0")
                {
                    Label lbTitle = new Label();
                    Label lbContent = new Label();
                    StringBuilder sb = new StringBuilder();
                    DataRow[] parentMenus = dtMenu.Select("MsMenuItem_Id_0 = " + dtMenu.Rows[i]["MsMenuItem_Id"].ToString());
                    String UnOrderLsit = GenerateUL(parentMenus, dtMenu, sb);
                    lbTitle.Text = dtMenu.Rows[i]["title"].ToString();
                    //TreeView TrV = GenerateTree(parentMenus, dtMenu, lbTitle.Text, i);
                    //TrV.ID = "TreeView" + i;

                    lbContent.Text = UnOrderLsit ;
                    pn = new AjaxControlToolkit.AccordionPane();
                    pn.ID = "Pane" + i;
                    pn.HeaderContainer.Controls.Add(lbTitle);
                    pn.ContentContainer.Controls.Add(lbContent);
                    acrDynamic.Panes.Add(pn);

                }
            }
        }
        
        // Code next..not used
        //if (LNumValid)
        //{
        //    if (!File.Exists(XMLFileName))
        //    {
        //        oMDBF.WriteXMLNew(AppId, oMDBF.Loginid, XMLFileName);
        //    }
        //    else
        //    {
        //        File.Delete(XMLFileName);
        //        oMDBF.WriteXMLNew(AppId, oMDBF.Loginid, XMLFileName);
        //    }
        //    MySource.DataFile = XMLFileName;
        //    TreeNodeBinding tNode = new TreeNodeBinding();
        //    tNode.DataMember = "MsMenuItem";
        //    tNode.TextField = "title";
        //    tNode.TargetField = "result";
        //    tNode.NavigateUrlField = "url";
        //    tNode.ValueField = "menuid";
        //    //tNode.ImageUrlField = "imageUrl";
        //    TreeView2.DataBindings.Add(tNode);
        //}
        //else
        //{
        //    MySource.DataFile = XMLFileName;
        //    TreeNodeBinding tNode = new TreeNodeBinding();
        //    tNode.DataMember = "MsMenuItem";
        //    tNode.TextField = "title";
        //    tNode.TargetField = "result";
        //    tNode.NavigateUrlField = "url";
        //    tNode.ValueField = "menuid";

        //    //tNode.ImageUrlField = "imageUrl";
        //    TreeView2.DataBindings.Add(tNode);
        //}
        Password = "";

        //GetAccordionMenu();
    }

    protected TreeView GenerateTree(DataRow[] menu, DataTable table, String sb,int ID)
    {
        DataTable dtTree = new DataTable();
        dtTree = menu.CopyToDataTable();
        MySource.Data = ConvertDatatableToXML(dtTree,table,sb);
 
        TreeView tv = new TreeView();
        tv.ID = "Treeview" + ID;
        TreeProperties(tv);

        TreeNodeBinding tNode = new TreeNodeBinding();
        tNode.DataMember = "MsMenuItem";
        tNode.TextField = "title";
        tNode.TargetField = "result";
        tNode.NavigateUrlField = "url";
        tNode.ValueField = "menuid";
        //tNode.ImageUrlField = "imageUrl";
        tv.DataBindings.Add(tNode);

        return tv;
    }
    public string ConvertDatatableToXML(DataTable dtTree,DataTable dtMain, String Filename)
    {
        dtTree.TableName = "MyTable";
        dtMain.TableName = "MyMainTable";
        String XMLFile = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "XML\\" + Filename + oMDBF.Loginid + ".xml";

        String LStrXml = "";
        LStrXml = "<?xml version='1.0'?>\n<MsMenu>\n";        
        createXmlItem(ref LStrXml, dtTree, dtMain, dtTree.Rows.Count, Filename);
        LStrXml = LStrXml + "</MsMenu>";

        return (LStrXml);
    }
    private void createXmlItem(ref string xmlString, DataTable dtSelect, DataTable dtMain, int nRows, string fileName)
    {
        for (int i = 0; i < nRows; i++)
        {
            DataRow[] LObjDataMenuFounds;
            String MenuID = dtSelect.Rows[i]["menuid"].ToString();
            string parentID = dtSelect.Rows[i]["MsMenuItem_Id"].ToString();
            xmlString = xmlString + "<MsMenuItem title='" + dtSelect.Rows[i]["title"].ToString() + "' url='" + dtSelect.Rows[i]["url"].ToString() + "' result='" + dtSelect.Rows[i]["result"].ToString() + "' menuid='" + MenuID + "' >";
            LObjDataMenuFounds = dtMain.Select(String.Format("MsMenuItem_Id_0 = {0}", parentID));
            if (LObjDataMenuFounds.Length > 0)
            {
                DataTable dtMenuFounds = LObjDataMenuFounds.CopyToDataTable();
                createXmlItem(ref xmlString, dtMenuFounds, dtMain, dtMenuFounds.Rows.Count, fileName);
            }
            xmlString = xmlString + "</MsMenuItem>\n";
        }
    }
    private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
    {
	    sb.AppendLine("<ul>");
	    if (menu.Length > 0) {
		    foreach (DataRow dr in menu) {
			    //Dim handler As String = dr("Handler").ToString()
			    string menuText = dr["title"].ToString();
                string url = dr["url"].ToString();
                string target = "";
                if(url != "#" )
                {
                    target = "ContentFrame";
                }
			    //string line = String.Format("<li><a href="#">{0}</a>", menuText);
                string line = String.Format("<li><div class='menu_body'><a href=" + url + " target =" + target + ">{0}</a></div>", menuText);  
			    sb.Append(line);

                string pid = dr["MsMenuItem_Id"].ToString();

                DataRow[] subMenu = table.Select(String.Format("MsMenuItem_Id_0 = {0}", pid));
			    if (subMenu.Length > 0) {
				    dynamic subMenuBuilder = new StringBuilder();
				    sb.Append(GenerateUL(subMenu, table, subMenuBuilder));
			    }
			    sb.Append("</li>");
		    }
	    }
	    sb.Append("</ul>");
	    return sb.ToString();
    }
   
    protected void TreeView2_TreeNodeDataBound(object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
    {
        if (e.Node.Target == "SM")
        {
            e.Node.SelectAction = TreeNodeSelectAction.None;
            e.Node.Target = "ContentFrame";
           
        }
        else
        {
            e.Node.Target = "ContentFrame";
        }
        if (e.Node.Text == "MsMenu")
        {
            e.Node.Text = "Menu";
            e.Node.SelectAction = TreeNodeSelectAction.None;
        }
    }

    protected void TreeProperties(TreeView tvw)
    {
        tvw.DataSourceID = "MySource";
        tvw.ExpandDepth = 1;
        tvw.MaxDataBindDepth = 10;
        tvw.CssClass = "page";
        tvw.ForeColor = System.Drawing.Color.Black;
        tvw.NodeIndent = 10;
        tvw.Width = 105;
        tvw.LineImagesFolder = "~/TreeLineImages";
        tvw.ImageSet = TreeViewImageSet.Arrows;
        tvw.ParentNodeStyle.HorizontalPadding = 5;
        tvw.ParentNodeStyle.Font.Underline = false;
        tvw.ParentNodeStyle.ForeColor = System.Drawing.Color.Blue;
        tvw.HoverNodeStyle.Font.Underline = true;
        tvw.HoverNodeStyle.Font.Size = 10;
        tvw.HoverNodeStyle.ForeColor = System.Drawing.Color.Red;
        tvw.SelectedNodeStyle.Font.Underline = true;
        tvw.SelectedNodeStyle.HorizontalPadding = 0;
        tvw.SelectedNodeStyle.VerticalPadding = 0;
        tvw.SelectedNodeStyle.ForeColor = System.Drawing.Color.Black;
        tvw.RootNodeStyle.HorizontalPadding = 0;
        tvw.RootNodeStyle.Font.Underline = false;
        tvw.RootNodeStyle.ForeColor = System.Drawing.Color.Blue;
        tvw.RootNodeStyle.Font.Bold = true;
        tvw.NodeStyle.Font.Size = 8;
        tvw.NodeStyle.ForeColor = System.Drawing.Color.Blue;
        tvw.NodeStyle.HorizontalPadding = 10;
        tvw.NodeStyle.NodeSpacing = 0;
        tvw.NodeStyle.VerticalPadding = 0;
    }

}

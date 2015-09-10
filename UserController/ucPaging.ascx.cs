using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public partial class usercontroller_ucPaging : UserControl
{
        public event NavigationButtonClick NavigationButtonClicked;        

        public long PageSize
        {
            get { return ((long)ViewState["PageSize"]); }
            set { ViewState["PageSize"] = value; }
        }
        public int currentPage
        {
            get { return ((int)ViewState["currentPage"]); }
            set { ViewState["currentPage"] = value; }
        }
        public double totalPages
        {
            get { return ((double)ViewState["totalPages"]); }
            set { ViewState["totalPages"] = value; }
        }
        public string cmdWhere
        {
            get { return ((string)ViewState["cmdWhere"]); }
            set { ViewState["cmdWhere"] = value; }
        }
        public string sortBy
        {
            get { return ((string)ViewState["sortBy"]); }
            set { ViewState["sortBy"] = value; }
        }
        public long totalRecords
        {
            get { return ((long)ViewState["totalRecords"]); }
            set { ViewState["totalRecords"] = value; }
        }

        //Public Event NavigationButtonClicked As NavigationButtonClick

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected virtual void OnNavigationButtonClicked(NavigationButtonEventArgs e)
        {
            if (NavigationButtonClicked != null)
            {
                NavigationButtonClicked(e);
            }
        }

        protected void btnFirst_Click(object sender, ImageClickEventArgs e)
        {
            NavigationButtonEventArgs oEventArgs = new NavigationButtonEventArgs();
            oEventArgs.ButtonClicked = ClickedButton.FirstRecordButton;            
            txtGO.Text = "1";
            oEventArgs.PageToGo = 1;
            NavigationLink("First");
            OnNavigationButtonClicked(oEventArgs);
        }

        protected void btnPrev_Click(object sender, ImageClickEventArgs e)
        {
            NavigationButtonEventArgs oEventArgs = new NavigationButtonEventArgs();
            oEventArgs.ButtonClicked = ClickedButton.PreviousRecordButton;            
            NavigationLink("Prev");
            oEventArgs.PageToGo = currentPage;
            OnNavigationButtonClicked(oEventArgs);            
        }

        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            NavigationButtonEventArgs oEventArgs = new NavigationButtonEventArgs();
            oEventArgs.ButtonClicked = ClickedButton.NextRecordButton;            
            NavigationLink("Next");
            oEventArgs.PageToGo = currentPage;
            OnNavigationButtonClicked(oEventArgs);
        }

        protected void btnLast_Click(object sender, ImageClickEventArgs e)
        {
            NavigationButtonEventArgs oEventArgs = new NavigationButtonEventArgs();
            oEventArgs.ButtonClicked = ClickedButton.LastRecordButton;            
            NavigationLink("Last");
            oEventArgs.PageToGo = Convert.ToInt16(totalPages.ToString());
            txtGO.Text = totalPages.ToString();
            OnNavigationButtonClicked(oEventArgs);
        }

        protected void btnGO_Click(object sender, ImageClickEventArgs e)
        {
            NavigationButtonEventArgs oEventArgs = new NavigationButtonEventArgs();
            oEventArgs.ButtonClicked = ClickedButton.GoToPageButton; 
            int iPageNo;

            try
            {
                iPageNo = int.Parse(txtGO.Text.ToString());
            }
            catch (Exception ex)
            {
                iPageNo = currentPage;
            }
            if(iPageNo > totalPages)
            {
                iPageNo = int.Parse(totalPages.ToString());
            }
            oEventArgs.PageToGo = iPageNo;
            txtGO.Text = totalPages.ToString();
            if(IsNumeric(txtPagesize.Text) == false)
            {
                txtPagesize.Text = "20";
            }
            PageSize = long.Parse(txtPagesize.Text.ToString());
            OnNavigationButtonClicked(oEventArgs);
        }

        protected bool IsNumeric(String Txt)
        {
            bool fnIsVal_Number = false;
            string mlVALUE = null;
            string mlPATTERN = null;
            Match mlMATCH = default(Match);

            fnIsVal_Number = false;
            mlVALUE = Txt;
            mlPATTERN = "^[0-9]+$";
            mlMATCH = Regex.Match(mlVALUE.Trim(), mlPATTERN, RegexOptions.IgnoreCase);

            if ((mlMATCH.Success) == true)
            {
                fnIsVal_Number = true;
            }
            else
            {
                fnIsVal_Number = false;
            }
            return fnIsVal_Number;

        }

        protected void NavigationLink(String BtnName)
        {
            switch (BtnName)
            {
                case "First":
                    currentPage = 1;
                    break;
                case "Prev":
                    currentPage = Int32.Parse(lblCurrentPage.Text) - 1;
                    break;
                case "Next":
                    currentPage = Int32.Parse(lblCurrentPage.Text) + 1;
                    break;
                case "Last":
                    currentPage = Int32.Parse(lblTotalPage.Text);
                    break;
            }
        }

        public Boolean PagingFooter(int iTotalRecord , int iPageSize)
        {
            Boolean PagingTes = false;

            totalRecords = long.Parse(iTotalRecord.ToString());
            PageSize = long.Parse(iPageSize.ToString());

            lblCurrentPage.Text = currentPage.ToString();
            txtGO.Text = currentPage.ToString();
            totalPages = Math.Ceiling(double.Parse(totalRecords.ToString()) / double.Parse(PageSize.ToString()));
            if (totalPages == 0)
            {
                lblTotalPage.Text = "1";
                //rgvGo.MaximumValue = "1";
            }
            else
            {
                lblTotalPage.Text = (System.Math.Ceiling(totalPages)).ToString();
                //rgvGo.MaximumValue = (System.Math.Ceiling(totalPages)).ToString();
            }
            lblTotalRec.Text = totalRecords.ToString();

            if (currentPage == 1)
            {
                btnPrev.Enabled = false;
                btnFirst.Enabled = false;
                if (totalPages > 1)
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                }
                else
                {
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnFirst.Enabled = false;
                }
            }
            else
            {
                btnPrev.Enabled = true;
                btnFirst.Enabled = true;
                if (currentPage == totalPages)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnLast.Enabled = true;
                    btnNext.Enabled = true;
                }
            }

            PagingTes = true;

            return PagingTes;
        }

        //Protected Overridable Sub OnNavigationButtonClicked(ByVal e As NavigationButtonEventArgs)
        //RaiseEvent NavigationButtonClicked(e)
        //End Sub
        //protected override void OnNavigationButtonClicked (NavigationButtonEventArgs e)
        //{
        //    RaiseKeyEvent(this, NavigationButtonClicked(e));
        //}
        public enum ClickedButton : int
        {
            FirstRecordButton,
            PreviousRecordButton,
            NextRecordButton,
            LastRecordButton,
            GoToPageButton
        };    
        public class NavigationButtonEventArgs 
        {
            ClickedButton _ClickedButton = ClickedButton.FirstRecordButton;
            int _PageToGo = 1;

            public ClickedButton ButtonClicked
            {
                get { return _ClickedButton; }
                set { _ClickedButton = value; }
            }
            public int PageToGo
            {
                get {return _PageToGo; }
                set {_PageToGo = value; }
            }
        }
        public delegate void NavigationButtonClick(NavigationButtonEventArgs e);
        
}

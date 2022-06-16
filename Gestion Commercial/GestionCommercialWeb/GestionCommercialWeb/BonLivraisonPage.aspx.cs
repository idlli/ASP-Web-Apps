using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionCommercialWeb
{
    public partial class BonLivraisonPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BonLivraisonR bonL = new BonLivraisonR();
            bonL.SetDataSource(g.dc.BonLivraisons.Where(o => o.NumLiv == Session["NLivraison"].ToString()));
            CrystalReportViewer1.ReportSource = bonL;
            CrystalReportViewer1.DataBind();
        }
    }
}
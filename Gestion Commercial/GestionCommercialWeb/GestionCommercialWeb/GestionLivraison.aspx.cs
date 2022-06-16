using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionCommercialWeb
{
    public class MyTextField : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            var txtBox = new TextBox { ID = "TextBox4" };
            container.Controls.Add(txtBox);
        }
    }

    public class MyGridLivraison
    {
        public string CodeArt { get; set; }
        public string Desi { get; set; }
        public decimal? PU { get; set; }
        public int? Qte { get; set; }

        public MyGridLivraison(string codeArt, string desi, decimal? pU, int? qte)
        {
            CodeArt = codeArt;
            Desi = desi;
            PU = pU;
            Qte = qte;
        }
    }
    public partial class GestionLivraison : System.Web.UI.Page
    {
        List<MyGridLivraison> obj = new List<MyGridLivraison>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                obj.Clear();
            
                TextBox1.Text = Session["NCommande"].ToString();

                var _commande = g.dc.Commandes.Where(o => o.NumCom == TextBox1.Text).FirstOrDefault();
                if (_commande != null)
                {
                    var _details = g.dc.Details.Where(o => o.NumCom == TextBox1.Text).Select(o => new { o.CodeArt, o.Article.Desi, o.Article.PU, o.Qte });
                    foreach (var item in _details)
                    {
                        obj.Add(new MyGridLivraison(item.CodeArt, item.Desi, item.PU, item.Qte ));
                    }
                    GridView1.DataSource = obj;
                    TemplateField temp = new TemplateField();
                    temp.HeaderText = "QL";
                    temp.ItemTemplate = new MyTextField();
                    GridView1.Columns.Add(temp);
                    GridView1.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var _livraison = g.dc.Livraisons.Where(o => o.NumLiv == TextBox2.Text).FirstOrDefault();
            if (_livraison == null)
            {
                Livraison _liv = new Livraison();
                _liv.NumLiv = TextBox2.Text;
                _liv.DateLiv = DateTime.Parse(TextBox3.Text);
                _liv.Livreur = TextBox4.Text;
                _liv.EtatLiv = false;
                g.dc.Livraisons.InsertOnSubmit(_liv);
                g.dc.SubmitChanges();
                _livraison = g.dc.Livraisons.Where(o => o.NumLiv == TextBox2.Text).FirstOrDefault();
            }
            using (var _tran = new TransactionScope())
            {
                try
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        int _ql;
                        if (int.TryParse(((TextBox)row.Cells[4].FindControl("TextBox4")).Text == null ? "" : ((TextBox)row.Cells[4].FindControl("TextBox4")).Text, out _ql) && int.Parse(row.Cells[3].ToString()) >= _ql)
                        {
                            var _getLigne = g.dc.LigneLivraisons.Where(o => o.NumLiv == _livraison.NumLiv && o.CodeArt == row.Cells[0].ToString()).FirstOrDefault();
                            if (_getLigne == null)
                            {
                                LigneLivraison _ligneLivraison = new LigneLivraison();
                                _ligneLivraison.NumLiv = _livraison.NumLiv;
                                _ligneLivraison.CodeArt = row.Cells[0].ToString();
                                _ligneLivraison.QLiv = _ql;
                                g.dc.LigneLivraisons.InsertOnSubmit(_ligneLivraison);
                                var _detail = g.dc.Details.Where(o => o.NumCom == TextBox1.Text && o.CodeArt == row.Cells[0].ToString()).FirstOrDefault();
                                _detail.Qte -= _ql;
                            }
                            else
                            {
                                var _detail = g.dc.Details.Where(o => o.NumCom == TextBox1.Text && o.CodeArt == row.Cells[0].ToString()).FirstOrDefault();
                                _detail.Qte = (_detail.Qte + _getLigne.QLiv) - _ql;
                                _getLigne.QLiv = _ql;
                            }
                        }
                    }
                    g.dc.SubmitChanges();
                    _tran.Complete();
                }
                catch
                {
                    Response.Write("<script>alert('Error dans l'enregistrement des livraisons')</script>");
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var _livraison = g.dc.Livraisons.Where(o => o.NumLiv == TextBox2.Text).FirstOrDefault();
            _livraison.EtatLiv = true;
            g.dc.SubmitChanges();
            Button1.Enabled = false;
            Button2.Enabled = false;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionCommandes.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["NLivraison"] = TextBox2.Text;
            Response.Redirect("BonLivraisonPage.aspx");
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            obj.Clear();
            if (TextBox2.Text.Any())
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    var _ligneLivraison = g.dc.LigneLivraisons.Where(o => o.NumLiv == TextBox2.Text && o.CodeArt == row.Cells[0].ToString()).FirstOrDefault();
                    if (_ligneLivraison != null)
                    {
                        ((TextBox)row.Cells[4].FindControl("TextBox4")).Text = _ligneLivraison.QLiv.ToString();
                    }
                }
                GridView1.DataBind();
            }
        }
    }
}
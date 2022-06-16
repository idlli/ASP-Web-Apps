using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

namespace GestionCommercialWeb
{
    class myGtidCommande
    {

        public myGtidCommande(string codeArt, string desi, decimal? pU, int? qte, decimal? montant)
        {
            CodeArt = codeArt;
            Desi = desi;
            PU = pU;
            this.montant = montant;
            Qte = qte;
        }

        public string CodeArt { get; set; }
        public string Desi { get; set; }
        public decimal? PU { get; set; }
        public decimal? montant { get; set; }
        public int? Qte { get; set; }

    }
    public partial class GestionCommandes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.DataSource = g.dc.Clients.Select(o => o.CodeCl);
                DropDownList1.DataBind();
                DropDownList2.DataSource = g.dc.Articles.Where(o => o.QDISP > 0).Select(o => o.CodeArt);
                DropDownList2.DataBind();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _client = g.dc.Clients.Where(o => o.CodeCl == DropDownList1.SelectedValue.ToString()).FirstOrDefault();
            if (_client != null)
            {
                TextBox3.Text = _client.Nom;
                TextBox4.Text = _client.Ville;
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _article = g.dc.Articles.Where(o => o.CodeArt == DropDownList2.SelectedValue.ToString()).FirstOrDefault();
            if (_article != null)
            {
                TextBox5.Text = _article.Desi;
                TextBox6.Text = _article.PU.ToString();
                TextBox7.Text = _article.QDISP.ToString();
            }
        }

        List<myGtidCommande> obj = new List<myGtidCommande>();
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            obj.Clear();
            Label1.Text = "";
            if (TextBox1.Text.Length > 0)
            {
                var _commande = g.dc.Commandes.Where(o => o.NumCom == TextBox1.Text).FirstOrDefault();
                if (_commande != null)
                {
                    TextBox2.Text = _commande.DateCom.Value.ToString();
                    var _details = g.dc.Details.Where(o => o.NumCom == TextBox1.Text).Select(o => new { o.CodeArt, o.Article.Desi, o.Article.PU, o.Qte, montant = o.Article.PU * o.Qte });
                    foreach (var item in _details)
                    {
                        obj.Add(new myGtidCommande( item.CodeArt, item.Desi, item.PU, item.Qte, item.montant ));
                    }
                    GridView1.DataSource = obj;
                    Label1.Text = _details.Sum(o => o.montant).ToString();
                    if (_commande.NE.Value)
                    {
                        Button1.Enabled = false;
                        Button2.Enabled = false;
                        Button3.Enabled = false;
                    }
                    else
                    {
                        Button1.Enabled = true;
                        Button2.Enabled = true;
                        Button3.Enabled = true;
                    }
                }
                else
                {
                    Button1.Enabled = true;
                    Button2.Enabled = true;
                    Button3.Enabled = true;
                }
            }
            GridView1.DataBind();
        }

        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            Button3.Enabled = DropDownList1.Text.Any();
            var _client = g.dc.Clients.Where(o => o.CodeCl == DropDownList1.Text).FirstOrDefault();
            if (_client != null)
            {
                TextBox3.Enabled = false;
                TextBox4.Enabled = false;
                DropDownList1_SelectedIndexChanged(null, e);
            }
            else
            {
                TextBox3.Enabled = true;
                TextBox4.Enabled = true;
            }
        }

        protected void DropDownList2_TextChanged(object sender, EventArgs e)
        {
            Button1.Enabled = DropDownList2.Text.Any();
            var _article = g.dc.Articles.Where(o => o.CodeArt == DropDownList2.Text).FirstOrDefault();
            if (_article != null)
            {
                TextBox5.Enabled = false;
                TextBox6.Enabled = false;
                TextBox7.Enabled = false;
                DropDownList2_SelectedIndexChanged(null, e);
            }
            else
            {
                TextBox5.Enabled = true;
                TextBox6.Enabled = true;
                TextBox7.Enabled = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            obj.Clear();
            decimal _pu;
            int _q;
            var _article = g.dc.Articles.Where(o => o.CodeArt == DropDownList2.Text).FirstOrDefault();
            if (_article == null)
            {
                Article _art = new Article();
                _art.CodeArt = DropDownList2.Text;
                _art.Desi = TextBox5.Text;
                _art.PU = decimal.Parse(TextBox6.Text);
                _art.QDISP = int.Parse(TextBox7.Text);
                g.dc.Articles.InsertOnSubmit(_art);
                _article = g.dc.Articles.Where(o => o.CodeArt == DropDownList2.Text).FirstOrDefault();
            }
            if (decimal.TryParse(TextBox6.Text.Trim(), out _pu) && _pu > 0 && int.TryParse(TextBox8.Text.Trim(), out _q) && _q > 0 && int.Parse(TextBox7.Text) >= _q)
            {

                obj.Add(new myGtidCommande(DropDownList2.Text, TextBox5.Text, _pu, _q, _pu * _q));
                GridView1.DataSource = obj;
                GridView1.DataBind();
                _article.QDISP -= _q;
                if (_article.QDISP <= 0)
                {
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    DropDownList2.DataSource = g.dc.Articles.Where(o => o.QDISP > 0).Select(o => o.CodeArt);
                    DropDownList2.DataBind();
                }
                else
                {
                    TextBox8.Text = _article.QDISP.ToString();
                }
                Label1.Text = ((Label1.Text.Any() ? decimal.Parse(Label1.Text) : 0) + _pu * _q).ToString();
            }
            else
            {
                Response.Write("<script>alert('Une error dans l'insertion car le pu ou q n'est pas correct')</script>");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button2.Enabled = GridView1.SelectedIndex > -1 ? true : false;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var _article = g.dc.Articles.Where(o => o.CodeArt == GridView1.SelectedRow.Cells[0].ToString()).FirstOrDefault();
            _article.QDISP += int.Parse(GridView1.SelectedRow.Cells[3].ToString());
            Label1.Text = (decimal.Parse(Label1.Text) - decimal.Parse(GridView1.SelectedRow.Cells[4].ToString())).ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var _client = g.dc.Clients.Where(o => o.CodeCl == DropDownList1.Text).FirstOrDefault();
            if (_client == null)
            {
                Client _cli = new Client();
                _cli.CodeCl = DropDownList1.Text;
                _cli.Nom = TextBox3.Text;
                _cli.Ville = TextBox4.Text;
                g.dc.Clients.InsertOnSubmit(_cli);
                _client = g.dc.Clients.Where(o => o.CodeCl == DropDownList1.Text).FirstOrDefault();
            }
            using (var _tran = new TransactionScope())
            {
                try
                {
                    g.dc.SubmitChanges();
                    var _comm = g.dc.Commandes.Where(o => o.NumCom == TextBox1.Text).FirstOrDefault();
                    if (_comm == null)
                    {
                        Commande _commande = new Commande();
                        _commande.NumCom = TextBox1.Text;
                        _commande.DateCom = DateTime.Parse(TextBox2.Text);
                        _commande.CodeCl = _client.CodeCl;
                        g.dc.Commandes.InsertOnSubmit(_commande);
                        g.dc.SubmitChanges();
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        var _getDet = g.dc.Details.Where(o => o.NumCom == TextBox1.Text && o.CodeArt == row.Cells[0].ToString()).FirstOrDefault();
                        if (_getDet == null)
                        {
                            var _detail = new Detail();
                            _detail.NumCom = TextBox1.Text;
                            _detail.CodeArt = row.Cells[0].ToString();
                            _detail.Qte = int.Parse(row.Cells[3].ToString());
                            g.dc.Details.InsertOnSubmit(_detail);
                        }
                    }
                    g.dc.SubmitChanges();
                    _tran.Complete();
                }
                catch
                {
                    Response.Write("<script>alert('Error dans l'enregistrement de commande')</script>");
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            var _comm = g.dc.Commandes.Where(o => o.NumCom == TextBox1.Text).FirstOrDefault();
            _comm.NE = true;
            g.dc.SubmitChanges();
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button3.Enabled = false;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            var _commande = g.dc.Commandes.Where(o => o.NumCom == TextBox1.Text).FirstOrDefault();
            if (_commande != null && _commande.NE.Value)
            {
                Session["NCommande"] = TextBox1.Text;
                Response.Redirect("GestionLivraison.aspx");
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Session["NCommande"] = TextBox1.Text;
            Response.Redirect("BonCommandePage.aspx");
        }
    }
}
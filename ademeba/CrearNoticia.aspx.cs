using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace ademeba
{
    public partial class CrearNoticia : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //row 1
            TableRow idRow = new TableRow();
            TableCell idTag = new TableCell();
            TableCell idInput = new TableCell();
            idTag.Text = "ID";
            TextBox idBox = new TextBox();
            idInput.Controls.Add(idBox);
            idRow.Cells.Add(idTag);
            idRow.Cells.Add(idInput);
            createIdTable.Rows.Add(idRow);
            //row 2
            TableRow Title = new TableRow();
            TableCell TitleTag = new TableCell();
            TableCell TitleInput = new TableCell();
            TitleTag.Text = "Titulo";
            TextBox titleBox = new TextBox();
            TitleInput.Controls.Add(titleBox);
            Title.Cells.Add(TitleTag);
            Title.Cells.Add(TitleInput);
            createIdTable.Rows.Add(Title);
            //row 3
            TableRow Text = new TableRow();
            TableCell TextTag = new TableCell();
            TextTag.Text = "Texto";
            TableCell TextInput = new TableCell();
            TextBox textInput = new TextBox();
            TextInput.Controls.Add(textInput);
            Text.Cells.Add(TextTag);
            Text.Cells.Add(TextInput);
            createIdTable.Rows.Add(Text);
            if (IsPostBack)
            {
                if(Session.Contents.Count != 0)
                {
                    Table reload = (Table)Session["imageTable"];
                    createIdTable.Dispose();
                    int flag = reload.Rows.Count;
                    while(reload.Rows.Count != 3)
                    {
                        createIdTable.Rows.Add(reload.Rows[3]);
                    }
                }

            }
            else
            {
                
            }
            LoadNews();
        }
        protected void saveNew(object sender, EventArgs e)
        {
            List<string> data = new List<string>();
            string save = "";
            
            foreach(TableRow row in createIdTable.Rows)
            {
                bool flag = false;
                foreach(TableCell cell in row.Cells)
                {
                    if (flag)
                    {
                        ControlCollection controls = cell.Controls;
                        foreach(Control control in controls)
                        {
                            TextBox box = (TextBox)control;
                            save += box.Text + "|";
                        }
                    }
                    flag = true;
                }
            }
            
            /*
            TextBox tb = (TextBox)(Table1.FindControl("TableRow1").FindControl("TableCell1").FindControl("tb1"));
            SqlDataSource1.InsertParameters["P"].DefaultValue = tb.Text;
             */
            save = save.Remove(save.Length - 1);
            /*
            ControlCollection controls = createIdTable.Rows[0].Cells[1].Controls;
            string test = controls.ToString();
            foreach(Control control in controls)
            {
                TextBox box = (TextBox)control;
                test = box.Text;
            }
            */
            //TextBox id = (TextBox)createIdTable.Rows[0].Cells[1].FindControl("idBox");
            string newJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Noticias", "Noticias.json");
            File.AppendAllText(newJsonPath, Environment.NewLine+ save);
            Response.Redirect("CrearNoticia.aspx");
        }
        protected void newImage(object sender, EventArgs e)
        {
            
            int pos = createIdTable.Rows.Count;
            TableRow row = new TableRow();
            TableCell blank = new TableCell();
            blank.Text = "Url imagen";
            row.Cells.Add(blank);
            TableCell imageurl = new TableCell();
            imageurl.ColumnSpan = 1;
            TextBox imageInput = new TextBox();
            imageInput.ID = "image" + pos.ToString();
            imageurl.Controls.Add(imageInput);
            row.Cells.Add(imageurl);
            createIdTable.Rows.Add(row);
            int posx = createIdTable.Rows.Count;
            Session.RemoveAll();
            Session.Add("imageTable", createIdTable);
        }
        protected void LoadNews()
        {
            List<string> data = arrangeData();
            foreach(string line in data)
            {
                string[] parser = line.Split('|');
                TableRow header = new TableRow();
                TableCell idTag = new TableCell();
                idTag.Text = "ID";
                header.Cells.Add(idTag);
                TableCell idValue = new TableCell();
                idValue.Text = parser[0];
                header.Cells.Add(idValue);
                newsTable.Rows.Add(header);
                TableRow noticia = new TableRow();
                TableCell titulo = new TableCell();
                titulo.Text = parser[1];
                noticia.Cells.Add(titulo);
                TableCell texto = new TableCell();
                texto.Text = parser[2];
                noticia.Cells.Add(texto);
                newsTable.Rows.Add(noticia);
                
                if (parser.Length > 3)
                {
                    for (int i = 3; i < parser.Length; i++)
                    {
                        TableRow images = new TableRow();
                        TableCell blank = new TableCell();
                        blank.Text = "Imagen";
                        images.Cells.Add(blank);
                        TableCell imageCell = new TableCell();
                        string fixSize = "style=" + '"' +"height:150px; width:auto;"+ '"';
                        imageCell.Text = string.Format("<img "+fixSize+" src=" + parser[i] + ">");
                        images.Cells.Add(imageCell);
                        newsTable.Rows.Add(images);

                    }
                }

            }
        }
        protected List<string> arrangeData()
        {
            string newJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Noticias", "Noticias.json");
            List<string> newData = new List<string>();
            if (File.Exists(newJsonPath))
            {
                StreamReader newFile = new StreamReader(newJsonPath);
                string line;
                while((line = newFile.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        newData.Add(line);
                    }
                    
                }
                newFile.Close();
            }
            
            return newData;
        }
        protected void overwrite(List<string> data)
        {
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Noticias", "Noticias.json"));
            File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Noticias", "Noticias.json"));
            StreamWriter file = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Noticias", "Noticias.json"));
            foreach(string line in data)
            {
                file.WriteLine(line);
            }
        }
        protected void formatData(DataTable data)
        {

        }
    }
}
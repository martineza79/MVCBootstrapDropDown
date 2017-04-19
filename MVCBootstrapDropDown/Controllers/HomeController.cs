using MVCBootstrapDropDown.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCBootstrapDropDown.Controllers
{
    public class HomeController : Controller
    {
        #region Index method
        // GET: Home
        public ActionResult Index()
        {
            // Initialize
            DropDownViewModel model = new DropDownViewModel();

            // Settings--Assigns the Selected CountryId property to the model object to zero
            model.SelectedCountryId = 0;

            this.ViewBag.CountryList = this.GetCountryList();

            // Loading drop down list
            return this.View(model);
        }

        #endregion
        #region Helpers

        #region Load CountryObj Data method
        // returns Data
        private List<CountryObj> LoadData()
        {
            // instantiate CountryObj list object
            List<CountryObj> lst = new List<CountryObj>();

            try
            {
                string line = string.Empty;
                string srcFilePath = "content/files/country_list.txt";
                var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase); //Path.GetDirectoryName derives from System.IO ----GetDirectoryName takes the string path parameter from the file or directory
                //Assembly class derives from System.Reflection to initialize a new instance of the Assembly class
                //GetExecutingAssembly() in Assebly gets the assembly that contains the code that is currently executing
                // CodeBase property {get; retrives} derives from the Assembly class to retrive data from the instantiated srcFilePath object/file
                var fullPath = Path.Combine(rootPath, srcFilePath);
                // Path.Combine(string path1, string path2) returns one of two string paths. In this case, the fullPath object is assigned and receives either the rootPath or srcFilePath file or directory
                string filePath = new Uri(fullPath).LocalPath; //LocalPath property {get;} returns the filename, in this case from the fullPath object
                StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));
                //StreamReader class instantiates the sr and assigns to the FileStream constructor that the string path, in this case from the filePath object, opens the object/file using the FileMode enumeration, and reads the object/file using the FileAccess enumeration

                // Read file
                while ((line = sr.ReadLine()) != null)
                {
                    //instantiate a CountryObj
                    CountryObj infoObj = new CountryObj();
                    string[] info = line.Split(','); //splits the data in the srcFilePath by a comma

                    infoObj.Country_Id = Convert.ToInt32(info[0].ToString());
                    infoObj.Country_Name = info[1].ToString();

                    lst.Add(infoObj);
                }

                sr.Dispose(); // close the srcFilePath
                sr.Close();
            }

            catch (Exception ex)
            {

                Console.Write(ex);
            }

            return lst;
        }
        #endregion
        #region Get roles method
        // this method returns the selected item(country) from the drop down list
        private IEnumerable<SelectListItem> GetCountryList() //SelectListItem from the System.Web.Mvc namespace
        {
            SelectList lstobj = null;

            try
            {
                var list = this.LoadData()
                    .Select(p => new SelectListItem { Value = p.Country_Id.ToString(), Text = p.Country_Name });
                //loads data from the LoadData() and returns the value based on the user's selection
                lstobj = new SelectList(list, "Value", "Text"); //SelectList method derives from the SelectListItem class and initializes a new IEnumerable instance, using the data value field, and data text field
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return lstobj;
        }

        #endregion
        #endregion
    }
}
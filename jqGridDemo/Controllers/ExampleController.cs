using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jqGridDemo.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index(JqgridBaseRequest request)
        {

            var rows = new List<JqgridDemoModel>();
            var sidx = (request.page - 1) * request.rows;
            for (var i = 0; i < request.rows; i++)
            {
                var orderId = sidx + i + 1;
                rows.Add(new JqgridDemoModel()
                {
                    OrderID = orderId,
                    CustomerID = $"CustomerID{orderId}",
                    Freight = new Random().NextDouble(),
                    ShipName = $"ShipName{orderId}"
                });
            }

            var total = 500;
            var res = new JqgridResponse<JqgridDemoModel>()
            {
                page = request.page,
                total = total,
                records = (request.rows * total).ToString(),
                rows = rows
            };

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Xjson(string callback, object model)
        {
            return new ContentResult()
            {
                ContentType = "text/x-json",
                Content = $"{callback}({Newtonsoft.Json.JsonConvert.SerializeObject(model)})"
            };

        }
    }
}

public class JqgridResponse<T>
{
    public string callback { get; set; }
    public int page { get; set; }
    public string records { get; set; }
    public IEnumerable<T> rows { get; set; }
    public int total { get; set; }
}

public class JqgridDemoModel
{
    public int OrderID { get; set; }
    public string CustomerID { get; set; }
    public DateTime OrderDate { get; set; }
    public double Freight { get; set; }
    public string ShipName { get; set; }
}

public class JqgridBaseRequest
{
    public string callback { get; set; }
    public int rows { get; set; }
    public int page { get; set; }
    public string sidx { get; set; }
    public string sord { get; set; }
}

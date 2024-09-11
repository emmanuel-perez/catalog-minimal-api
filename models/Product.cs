

using System.ComponentModel;


namespace MinimalCatalogApi.Models {
    public class Product {

        public int product_id {get;set;}
        public string product_name {get; set;}
        public decimal product_price {get; set;}
        [DefaultValue(true)]
        public bool ActiveOnDb {get;set;}

        public Product() {
            if (product_name == null){
                product_name="";
            }
        }
    }
}













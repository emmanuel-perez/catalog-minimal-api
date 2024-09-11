

public class Product {

    int product_id {get;set;}
    string product_name {get; set;}
    decimal product_price {get; set;}

    public Product() {
        if (product_name == null){
            product_name="";
        }
    }

}













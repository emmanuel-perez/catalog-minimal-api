

public class UpsertProductDto {
    public string product_name {get; set;}
    public decimal product_price {get; set;}

    public UpsertProductDto() {
        if (product_name == null){
            product_name="";
        }
    }

}













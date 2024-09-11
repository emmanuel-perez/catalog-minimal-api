
using Dapper;
using Microsoft.Data.SqlClient;
using MinimalCatalogApi.Contracts;
using MinimalCatalogApi.Models;

namespace MinimalCatalogApi.Repositories {
    public class ProductRepository: IProductRepository {
        
        private readonly string _connectionString;

        public ProductRepository (string connectionString){
            _connectionString = connectionString;
        } 

        public async Task<IEnumerable<Product>>GetProducts(){
            string query = "SELECT * FROM PRODUCTS";
            using (var connection = new SqlConnection(_connectionString)){
                try
                {
                    return await connection.QueryAsync<Product>(query);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }   

        public async Task<Product>GetProductById(int productId){
            string query = @"
                SELECT * FROM CatalogShema.Products
                WHERE product_id = @Id
            ";
            using (var connection = new SqlConnection(_connectionString)){
                try
                {
                    return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = productId });
                }
                catch (Exception ex)
                {   
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<bool>CreateProduct(UpsertProductDto productToCreate){
            string sql = @"
                INSERT INTO CatalogSchema.Products (
                    product_name,
                    product_price
                ) VALUES(
                    @ProductName,
                    @ProductPrice
                )
            ";
            using (var connection = new SqlConnection(_connectionString)){
                try
                {
                    var affectedRows = await connection.ExecuteAsync(sql, new {
                        ProductName = productToCreate.product_name,
                        ProductPrice = productToCreate.product_price
                    });

                    return affectedRows > 0;   
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }

        public async Task<bool>UpdateProductById(int productId, UpsertProductDto productToUpdate){
            string sql = @"
                UPDATE CatalogSchema.Products
                SET 
                    product_name = @ProductName, 
                    product_price = @ProductPrice
                WHERE product_id = @ProductId AND 
            ";
            using (var connection = new SqlConnection(_connectionString)){

                try
                {
                    var affectedRows = await connection.ExecuteAsync(sql, new { 
                        @ProductName = productToUpdate.product_name, 
                        @ProductPrice = productToUpdate.product_price 
                    });

                    return affectedRows > 0;   
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            };
        }

        public async Task<bool>DeactivateProductById(int productId){
            string sql = @"
                UPDATE CatalogSchema.Products
                SET 
                    product_active = 0, 
                WHERE product_id = @ProductId AND product_active = 1; 
            ";
            using (var connection = new SqlConnection(_connectionString)){
                try
                {
                    var affectedRows = await connection.ExecuteAsync(sql, new { ProductId = productId });
                    return affectedRows > 0;   
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
    }
}














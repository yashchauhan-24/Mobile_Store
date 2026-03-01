# Mobile Store - Navigation Reference

## Frontend (Customer) URLs

### Home & Products
- **Home Page:** `/` or `/Home/Index`
- **All Products:** `/Product/Index`
- **Products by Category:** `/Product/Index?categoryId={id}`
- **Product Details:** `/Product/Details/{id}` or `/Home/Details/{id}`
- **Search Products:** `/Home/Search?q={query}`
- **Category Products:** `/Home/Category?id={categoryId}`
- **About Us:** `/Home/About`
- **Privacy Policy:** `/Home/Privacy`

### Shopping & Orders
- **Shopping Cart:** `/Cart/Index`
- **Wishlist:** `/Wishlist/Index`
- **Checkout:** `/Checkout/Index`
- **My Orders:** `/Checkout/Orders`
- **Order Success:** `/Checkout/Success/{orderId}`

### Account
- **Login:** `/Account/Login`
- **Register:** `/Account/Register`
- **Logout:** POST to `/Account/Logout`
- **Access Denied:** `/Account/AccessDenied`

## Admin Panel URLs

### Admin Dashboard
- **Admin Home:** `/Admin` or `/Admin/Dashboard/Index`

### Products Management
- **All Products:** `/Admin/Products/Index`
- **Create Product:** `/Admin/Products/Create`
- **Edit Product:** `/Admin/Products/Edit/{id}`
- **Delete Product:** POST to `/Admin/Products/Delete`

### Categories Management
- **All Categories:** `/Admin/Categories/Index`
- **Create Category:** `/Admin/Categories/Create`
- **Edit Category:** `/Admin/Categories/Edit/{id}`
- **Delete Category:** POST to `/Admin/Categories/Delete`

### Orders Management
- **All Orders:** `/Admin/Orders/Index`
- **Order Details:** `/Admin/Orders/Details/{id}`
- **Update Order Status:** POST to `/Admin/Orders/UpdateStatus`

### Users Management
- **All Users:** `/Admin/Users/Index`
- **Delete User:** POST to `/Admin/Users/Delete`

## API Endpoints (POST Actions)

### Cart Actions
- Add to Cart: POST `/Cart/Add` (productId, quantity)
- Update Quantity: POST `/Cart/UpdateQuantity` (id, qty)
- Remove Item: POST `/Cart/Remove` (id)
- Clear Cart: POST `/Cart/Clear`

### Wishlist Actions
- Add to Wishlist: POST `/Wishlist/Add` (productId)
- Remove from Wishlist: POST `/Wishlist/Remove` (id)
- Toggle Wishlist: POST `/Wishlist/Toggle` (productId)

### Checkout Actions
- Place Order: POST `/Checkout/PlaceOrder`

## Notes
- All admin pages require authentication (can be accessed without specific role for now)
- Cart, Wishlist, and Checkout require user authentication
- Product browsing and viewing is public (no authentication required)

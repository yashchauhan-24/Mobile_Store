# Quick Start Checklist - Before Running the App

## Pre-Run Checklist

### 1. Database Setup
- [ ] SQL Server LocalDB is installed
- [ ] Connection string is correct in `appsettings.json`
- [ ] Database will be created automatically on first run

### 2. Folder Structure
- [ ] `wwwroot` folder exists in project root
- [ ] `wwwroot/images` will be created automatically
- [ ] `wwwroot/css` folder exists with `site.css`
- [ ] `wwwroot/js` folder exists with required JavaScript files

### 3. Build and Run
```bash
# In Visual Studio
1. Clean Solution (Build ? Clean Solution)
2. Rebuild Solution (Build ? Rebuild Solution)
3. Press F5 to run
```

### 4. First Time Setup
When you run the app for the first time:
1. Database will be created automatically
2. `wwwroot/images` folder will be created
3. Go to `/Account/Register` to create an admin account
4. Login and access admin panel at `/Admin`

### 5. Testing Product Upload

#### Test 1: Add Product Without Image
1. Go to Admin ? Products ? Add Product
2. Fill in: Name, Price, Category
3. Click "Save Product"
4. ? Should succeed

#### Test 2: Add Product With Image
1. Go to Admin ? Products ? Add Product
2. Fill in: Name, Price, Category
3. Upload a valid image (JPG, PNG, GIF, WEBP under 5MB)
4. Click "Save Product"
5. ? Should succeed and show product with image

#### Test 3: Invalid File Type
1. Try to upload a .txt or .pdf file
2. ? Should show error: "Only image files allowed"

#### Test 4: Large File
1. Try to upload an image larger than 5MB
2. ? Should show error: "File size must be less than 5MB"

## Common Issues and Solutions

### Issue: "Cannot connect to database"
**Solution**: 
- Check if SQL Server LocalDB is running
- Run: `sqllocaldb start MSSQLLocalDB` in command prompt

### Issue: "Images folder not found"
**Solution**: 
- Restart the application - it will create the folder automatically
- Or manually create `wwwroot/images` folder

### Issue: "Access denied to images folder"
**Solution**:
- Right-click `wwwroot/images` folder
- Properties ? Security ? Edit
- Give full control to your user account

### Issue: Application crashes on product creation
**Solution**:
- Check the Output window in Visual Studio for error details
- Ensure all required fields are filled
- Ensure valid category is selected
- Check if database connection is working

## Admin Panel URLs
- Dashboard: `/Admin` or `/Admin/Dashboard/Index`
- Products: `/Admin/Products/Index`
- Categories: `/Admin/Categories/Index`
- Orders: `/Admin/Orders/Index`
- Users: `/Admin/Users/Index`

## Customer Panel URLs
- Home: `/` or `/Home/Index`
- Products: `/Product/Index`
- Cart: `/Cart/Index`
- Checkout: `/Checkout/Index`
- My Orders: `/Checkout/Orders`

## Default Configuration
- Max file size: 5MB
- Allowed image types: JPG, JPEG, PNG, GIF, WEBP
- Upload folder: `wwwroot/images`
- Database: SQL Server LocalDB

## Support
If you still experience issues after following this guide:
1. Check the detailed guide in `PRODUCT_IMAGE_UPLOAD_GUIDE.md`
2. Review error messages in Visual Studio Output window
3. Enable Developer Exception Page by ensuring you're running in Development mode

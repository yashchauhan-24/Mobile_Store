# Product Image Upload - Troubleshooting Guide

## Problem Fixed
The application was crashing when adding products with photos because:

1. **Missing Images Directory**: The `wwwroot/images` folder didn't exist
2. **No Error Handling**: Exceptions weren't being caught
3. **No File Validation**: Invalid files could cause crashes
4. **No Directory Creation**: Code didn't check if upload folder exists

## What Was Fixed

### 1. ProductsController.cs
- ? Added automatic directory creation if it doesn't exist
- ? Added comprehensive try-catch error handling
- ? Added file extension validation (only allows: jpg, jpeg, png, gif, webp)
- ? Added file size validation (max 5MB)
- ? Added proper error messages via TempData
- ? Added old image deletion when updating products
- ? Added image cleanup when deleting products

### 2. Program.cs
- ? Added automatic creation of `wwwroot/images` directory on startup
- ? Added file upload size limit configuration (10MB)
- ? Added error handling for database migrations
- ? Enabled DeveloperExceptionPage in development mode

### 3. Create/Edit Product Views
- ? Added client-side file validation (JavaScript)
- ? Added validation error display
- ? Added file type restrictions in HTML input
- ? Added helpful user messages about file requirements
- ? Added visual feedback for current images

### 4. appsettings.json
- ? Added file upload configuration settings
- ? Improved logging configuration

## How to Use

### Adding a New Product with Photo:

1. Go to Admin Panel ? Products ? Add Product
2. Fill in all required fields (marked with *)
3. Click "Choose File" for Product Image
4. Select an image file (JPG, PNG, GIF, or WEBP)
5. Ensure file is less than 5MB
6. Click "Save Product"
7. Product will be created with the image uploaded

### Allowed File Types:
- JPG / JPEG
- PNG
- GIF
- WEBP

### File Size Limit:
- Maximum: 5MB per image

## Error Messages You Might See

### "Only image files (jpg, jpeg, png, gif, webp) are allowed."
**Solution**: Select a valid image file format

### "File size must be less than 5MB."
**Solution**: Compress or resize your image to reduce file size

### "Error creating product: [error message]"
**Solution**: Check the error message for specific details. Common issues:
- Database connection problems
- Invalid category selected
- Missing required fields

## Troubleshooting Steps

### If the app still crashes:

1. **Check the images folder exists**:
   - Go to `wwwroot/images` in your project
   - If it doesn't exist, create it manually

2. **Check file permissions**:
   - Ensure the application has write permissions to the `wwwroot/images` folder

3. **Check the database**:
   - Ensure SQL Server LocalDB is running
   - Check connection string in `appsettings.json`

4. **Check the logs**:
   - Look in the Output window in Visual Studio
   - Check for any error messages

5. **Run in Development Mode**:
   - Set `ASPNETCORE_ENVIRONMENT=Development` to see detailed errors

6. **Clear and Rebuild**:
   ```
   - Clean Solution
   - Rebuild Solution
   - Run the application
   ```

## Testing Checklist

? Can create product without image
? Can create product with image
? Can edit product and change image
? Can edit product and keep existing image
? Can delete product (image is also deleted)
? File validation works (wrong file type rejected)
? File size validation works (large files rejected)
? Error messages display properly
? Success messages display properly

## Additional Notes

- Images are saved with unique GUID filenames to prevent conflicts
- Old images are automatically deleted when updating/deleting products
- The application automatically creates the images directory on startup
- All file operations have proper error handling
- Client-side validation provides immediate feedback
- Server-side validation ensures security

# Quick Testing Guide - User Profile System

## ?? Quick Start Testing

### Step 1: Register/Login
1. Run the application
2. Go to `/Account/Register` and create a new account
3. Login with your credentials

### Step 2: Test Profile Management
1. **Access Profile**
   - Click on your username (top-right dropdown)
   - Select "My Profile"
   - URL: `/Profile/Index`

2. **Update Profile**
   - Change your full name
   - Update email address
   - Add phone number
   - Fill in address fields:
     - Street Address: `123 Main St`
     - City: `New York`
     - State: `NY`
     - Zip Code: `10001`
     - Country: `United States`
   - Click "Save Changes"
   - ? Should see success message

3. **Change Password**
   - Click "Change Password" in sidebar
   - Enter current password
   - Enter new password (min 6 chars)
   - Confirm new password
   - Click "Change Password"
   - ? Should see success message

### Step 3: Test Order History
1. **Place an Order First**
   - Go to home page
   - Add a product to cart
   - Go to checkout
   - Place order

2. **View Order History**
   - Go to Profile ? My Orders
   - URL: `/Profile/Orders`
   - ? Should see your order listed

3. **View Order Details**
   - Click "View Details" button
   - ? Modal opens showing order items, total, status

### Step 4: Test Admin Panel
1. **Access Admin Panel**
   - Go to `/Admin`
   - URL: `/Admin/Dashboard/Index`

2. **Check Dashboard**
   - ? Recent orders show customer NAMES (not IDs)
   - ? Can see full customer name or email

3. **View Orders List**
   - Go to Admin ? Orders
   - URL: `/Admin/Orders/Index`
   - ? Each order shows:
     - Customer Name
     - Customer Email
     - Order date
     - Total amount
     - Status badge

4. **View Order Details**
   - Click "Details" on any order
   - URL: `/Admin/Orders/Details/{id}`
   - ? Should see:
     - Customer name
     - Customer email
     - Customer phone
     - Shipping address (if provided)
     - Product images
     - Order items
     - Total amount

5. **Update Order Status**
   - In order details, select new status
   - Click "Update Status"
   - ? Should see success message
   - ? Status badge updates

---

## ?? What to Check

### Profile Page (`/Profile/Index`)
- ? Sidebar shows user icon and username
- ? All menu items are clickable
- ? Form fields are pre-filled with current data
- ? Validation works (try empty name)
- ? Success message appears after save
- ? Email update changes username too

### Change Password (`/Profile/ChangePassword`)
- ? Requires current password
- ? New password must be min 6 chars
- ? Passwords must match
- ? Shows validation errors
- ? Success message appears
- ? Can login with new password

### My Orders (`/Profile/Orders`)
- ? Shows all user's orders
- ? Empty state message if no orders
- ? Status badges are color-coded:
  - ?? Green = Delivered
  - ?? Red = Cancelled
  - ?? Yellow = Processing/Shipped
- ? Modal opens on "View Details" click
- ? Modal shows all order items
- ? Can close modal

### Admin Orders List
- ? Customer NAME shown (not user ID)
- ? Email address visible
- ? Date formatted nicely
- ? Status badges color-coded
- ? Details button works

### Admin Order Details
- ? Customer section shows:
  - Full name
  - Email
  - Phone (or "N/A")
  - Full address (if provided)
- ? Product images display
- ? Order total calculates correctly
- ? Status dropdown works
- ? Back button returns to orders list

### Navigation
- ? User dropdown in header works
- ? Profile link goes to `/Profile/Index`
- ? My Orders link goes to `/Profile/Orders`
- ? Change Password link works
- ? Wishlist link works
- ? Logout button works

---

## ?? Common Issues & Solutions

### Issue: "Profile not found" error
**Solution**: Make sure you're logged in. Profile requires authentication.

### Issue: Email change fails
**Solution**: Email might already be in use by another account.

### Issue: Password change fails
**Solution**: Current password might be incorrect. Check caps lock.

### Issue: Orders don't show customer name
**Solution**: Make sure you ran the migration:
```bash
dotnet ef database update
```

### Issue: Address doesn't save
**Solution**: Check that all required fields (Name, Email) are filled.

### Issue: Admin sees user IDs instead of names
**Solution**: 
1. Rebuild the project
2. Restart the application
3. Check that User navigation property is included in queries

---

## ?? Test Scenarios

### Scenario 1: New User Profile Setup
1. Register new account
2. Login
3. Go to Profile
4. Add full address
5. Save
6. Verify address persists

### Scenario 2: Order Flow with Profile
1. Update profile with complete address
2. Add items to cart
3. Checkout
4. Place order
5. View in My Orders
6. Check admin sees your name and address

### Scenario 3: Profile Update
1. Login
2. Update name
3. Update email
4. Update phone
5. Update address
6. Save
7. Logout
8. Login with new email
9. Check all changes persisted

### Scenario 4: Password Change
1. Login
2. Change password
3. Logout
4. Try old password (should fail)
5. Login with new password (should work)

### Scenario 5: Admin Order Management
1. Login as admin
2. View orders list
3. Check customer names visible
4. Open order details
5. View customer info
6. Update order status
7. Verify status changed

---

## ? Success Criteria

All these should work without errors:
- ? Profile loads with correct data
- ? Profile updates successfully
- ? Address saves and displays
- ? Password changes work
- ? Order history displays
- ? Order details show in modal
- ? Admin sees customer names (not IDs)
- ? Admin sees customer emails
- ? Admin sees shipping addresses
- ? Order status updates work
- ? No compilation errors
- ? No runtime errors
- ? Database migration applied
- ? All navigation links work

---

## ?? You're All Set!

If all tests pass, your user profile management system is working perfectly!

**What Users Can Do:**
- ? Manage their complete profile
- ? Save shipping address
- ? Change password securely
- ? View complete order history
- ? Track order status

**What Admins Can Do:**
- ? See customer names in orders
- ? View customer contact details
- ? See shipping addresses
- ? Update order status
- ? Better customer service

**System Features:**
- ? Secure authentication
- ? Data validation
- ? Responsive design
- ? User-friendly interface
- ? Database persistence

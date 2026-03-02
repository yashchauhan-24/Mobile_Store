# Quick Payment System Testing Guide

## ?? Test the Payment System in 5 Minutes

### ? Test 1: Cash on Delivery (2 minutes)

**Steps:**
1. Login to your account
2. Add any product to cart
3. Click "Proceed to Checkout"
4. Fill in:
   - Name: John Doe
   - Phone: +91 9876543210
   - Address: 123 Main Street, Mumbai, Maharashtra, 400001
5. Keep "Cash on Delivery" selected (default)
6. Click "Place Order"

**Expected Results:**
- ? Redirected to success page
- ? Shows "Payment on Delivery"
- ? Payment Status: Pending
- ? No transaction ID
- ? Message: "Please keep Rs {amount} ready"

---

### ? Test 2: Online Payment (3 minutes)

**Steps:**
1. Login to your account
2. Add any product to cart
3. Click "Proceed to Checkout"
4. Fill in:
   - Name: Jane Smith
   - Phone: +91 9876543210
   - Address: 456 Park Avenue, Delhi, 110001
5. Select "Online Payment" radio button
6. ? **Verify:** Card form appears
7. Fill card details:
   - Card Number: **4111111111111111**
   - Card Holder: **Jane Smith**
   - Expiry Month: **12**
   - Expiry Year: **2025**
   - CVV: **123**
8. Click "Pay & Place Order"

**Expected Results:**
- ? Redirected to success page
- ? Shows "Online Payment"
- ? Payment Status: Paid (green badge)
- ? Transaction ID shown
- ? Success message displayed

---

### ? Test 3: Validation (1 minute)

**Test Empty Fields:**
1. Go to checkout
2. Click "Place Order" without filling anything
3. ? **Verify:** Error messages appear

**Test Card Validation:**
1. Select "Online Payment"
2. Try submitting with empty card details
3. ? **Verify:** Validation errors for card fields
4. Enter invalid card: 12345
5. ? **Verify:** Card number validation error
6. Enter invalid CVV: 12
7. ? **Verify:** CVV validation error

---

### ? Test 4: Admin View (1 minute)

**View Order in Admin Panel:**
1. Go to `/Admin/Orders/Index`
2. ? **Verify:** See payment method column
3. ? **Verify:** COD orders show ?? icon
4. ? **Verify:** Online orders show ?? icon
5. ? **Verify:** Payment status badges (Pending/Paid)
6. Click "Details" on any order
7. ? **Verify:** Payment method displayed
8. ? **Verify:** Payment status shown
9. ? **Verify:** Transaction ID (for online payments)
10. ? **Verify:** Customer name, phone, address shown

---

### ? Test 5: My Orders (1 minute)

**View Orders as Customer:**
1. Go to Profile ? My Orders
2. ? **Verify:** Orders listed
3. Click "View Details" on any order
4. ? **Verify:** Modal shows payment method
5. ? **Verify:** Modal shows payment status
6. ? **Verify:** Transaction ID shown (online payments)
7. ? **Verify:** Shipping address displayed

---

## ?? Visual Checks

### Checkout Page Should Show:
- ? Shipping information form (3 fields)
- ? Two payment option radio buttons
- ? Icons: ?? for COD, ?? for Online
- ? Card form hidden by default
- ? Card form appears when "Online Payment" selected
- ? Order summary sidebar with items
- ? Total amount clearly displayed
- ? "Secure Checkout" badge at bottom

### Success Page Should Show:
- ? Green checkmark icon
- ? "Order Placed Successfully!" heading
- ? Order number
- ? Order date
- ? Total amount
- ? Order status badge
- ? Payment method with icon
- ? Payment status badge (color-coded)
- ? Transaction ID (online only)
- ? Customer name and phone
- ? Shipping address
- ? Payment instruction message (COD) or success message (Online)
- ? Three action buttons

---

## ?? Test Card Numbers

Use these for testing:

| Card Number | CVV | Expiry | Result |
|-------------|-----|--------|--------|
| 4111111111111111 | 123 | 12/2025 | Success |
| 4242424242424242 | 123 | Any future | Success |
| 5555555555554444 | 456 | Any future | Success |

---

## ?? What to Look For

### ? Working Correctly:
- Payment method selection works
- Card form shows/hides correctly
- Button text changes ("Place Order" / "Pay & Place Order")
- Validation prevents empty submission
- Card validation works (13-16 digits, 3-4 CVV)
- Success page shows all details
- Transaction ID generated for online payments
- Payment status correct (Pending for COD, Paid for Online)
- Admin panel shows payment info
- My Orders shows payment details

### ? Issues to Report:
- Card form doesn't appear when selecting online payment
- Validation not working
- Success page missing information
- Transaction ID not generated
- Payment status incorrect
- Admin panel not showing payment method
- Database errors

---

## ?? Common Issues & Fixes

### Issue: Card form doesn't appear
**Fix:** Check JavaScript console for errors, ensure radio button has correct ID

### Issue: Validation not working
**Fix:** Ensure validation scripts are loaded (`_ValidationScriptsPartial`)

### Issue: Database error
**Fix:** Ensure migration was applied: `dotnet ef database update`

### Issue: Transaction ID not showing
**Fix:** Check Order model has TransactionId property and it's being set

---

## ?? Test Checklist

**Before Testing:**
- [ ] Database migration applied
- [ ] Application builds successfully
- [ ] Can login to customer account
- [ ] Can login to admin account
- [ ] Products exist in store

**Functional Tests:**
- [ ] COD order placement works
- [ ] Online payment order placement works
- [ ] Shipping info captured correctly
- [ ] Payment method saved correctly
- [ ] Payment status correct
- [ ] Transaction ID generated (online)
- [ ] Cart clears after order
- [ ] Success page displays correctly

**Validation Tests:**
- [ ] Empty field validation works
- [ ] Phone number validation works
- [ ] Card number validation works (online)
- [ ] CVV validation works (online)
- [ ] Expiry date validation works (online)

**Display Tests:**
- [ ] Checkout page layout correct
- [ ] Payment options display correctly
- [ ] Card form shows/hides properly
- [ ] Success page shows all details
- [ ] My Orders shows payment info
- [ ] Admin orders show payment method
- [ ] Admin order details show payment info

**Mobile Tests:**
- [ ] Checkout responsive on mobile
- [ ] Payment options work on mobile
- [ ] Card input works on mobile
- [ ] Success page responsive

---

## ?? Quick Test (30 seconds)

**Fastest Way to Test:**

1. Add product to cart ? Checkout
2. Fill name, phone, address
3. Keep COD selected ? Place Order
4. ? Check success page shows COD and Pending

5. Add product again ? Checkout
6. Select Online Payment ? Fill card: 4111111111111111, 123, 12/2025
7. Place Order
8. ? Check success page shows Online and Paid with Transaction ID

**Done! If both work, system is functioning correctly!** ??

---

## ?? URLs to Test

| Page | URL | What to Check |
|------|-----|---------------|
| Checkout | `/Checkout/Index` | Payment options, form |
| Success | `/Checkout/Success/{id}` | Payment details |
| My Orders | `/Profile/Orders` | Payment in modal |
| Admin Orders | `/Admin/Orders/Index` | Payment column |
| Admin Details | `/Admin/Orders/Details/{id}` | Payment section |

---

## ?? Success Criteria

? **System is working if:**
- Can place COD order successfully
- Can place online payment order successfully
- Payment details captured correctly
- Payment status correct
- Transaction ID generated for online
- Success page shows all information
- Admin can see payment details
- My Orders shows payment information

---

## ?? Support

If tests fail:
1. Check database migration applied
2. Check build successful
3. Check console for JavaScript errors
4. Check browser developer tools Network tab
5. Check server logs for errors

---

## ?? Expected Results Summary

**Cash on Delivery Order:**
```
? Order Created
? Payment Method: Cash on Delivery
? Payment Status: Pending
? Transaction ID: NULL
? Customer details saved
? Shipping address saved
```

**Online Payment Order:**
```
? Order Created
? Payment Method: Online Payment
? Payment Status: Paid
? Transaction ID: TXN{timestamp}
? Customer details saved
? Shipping address saved
```

---

## ?? Security Note

**This is a demo payment system.**

For production:
- Use real payment gateway (Razorpay, Stripe, PayU)
- Don't store card details
- Use HTTPS
- Implement proper security measures
- Add fraud detection
- Handle payment failures
- Implement refunds

---

**Happy Testing! ??**

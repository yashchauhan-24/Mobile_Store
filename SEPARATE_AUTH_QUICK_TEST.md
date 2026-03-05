# ?? Separate Authentication - Quick Test Guide

## ? **QUICK TEST**

Test that admin and client sessions are completely separated!

---

## ?? **5-Minute Test**

### **Test 1: Admin Login Doesn't Affect Client**

```
1. Open Incognito window
2. Go to: /Admin/Auth/Login
3. Login as admin
4. Open new tab (same browser)
5. Go to: / (home page)
6. Check navbar

Expected:
? Home page shows "Login" button
? User is NOT logged in
? Client side is independent

Cookie Check:
? Only .MobileStore.Admin cookie present
? No .MobileStore.Client cookie
```

---

### **Test 2: Client Login Doesn't Affect Admin**

```
1. Open Incognito window
2. Go to: /Account/Login
3. Register/Login as client
4. Open new tab (same browser)
5. Go to: /Admin/Dashboard
6. Expected: Redirects to admin login

Expected:
? Redirected to /Admin/Auth/Login
? Admin panel NOT accessible
? Admin side is independent

Cookie Check:
? Only .MobileStore.Client cookie present
? No .MobileStore.Admin cookie
```

---

### **Test 3: Both Logins Work Together**

```
1. Open browser
2. Go to: /Admin/Auth/Login
3. Login as admin
4. Go to admin dashboard - works ?
5. New tab: /Account/Login
6. Login as client
7. Go to home page - shows client email ?
8. Switch back to admin tab
9. Admin dashboard - still shows admin username ?

Expected:
? Admin panel shows admin username
? Client website shows client email
? Both sessions independent

Cookie Check:
? Both .MobileStore.Admin AND .MobileStore.Client present
```

---

### **Test 4: Admin Logout**

```
1. While logged in as both
2. In admin panel, click Logout
3. Go to client website (/)
4. Expected: STILL logged in as client ?

Result:
? Admin logged out
? Client STILL logged in
? Sessions independent
```

---

### **Test 5: Client Logout**

```
1. While logged in as both
2. On client website, click Logout
3. Go to: /Admin/Dashboard
4. Expected: STILL logged in as admin ?

Result:
? Client logged out
? Admin STILL logged in
? Sessions independent
```

---

## ?? **Check Browser Cookies**

### **How to Check:**
```
1. Open DevTools (F12)
2. Application tab
3. Cookies ? https://localhost:7048
```

### **When Admin Only:**
```
? .MobileStore.Admin (present)
? .MobileStore.Client (absent)
```

### **When Client Only:**
```
? .MobileStore.Admin (absent)
? .MobileStore.Client (present)
```

### **When Both:**
```
? .MobileStore.Admin (present)
? .MobileStore.Client (present)
```

---

## ? **Success Indicators**

### **Admin Panel:**
```
? Shows admin username in header
? Full access to all admin pages
? Can manage products, categories, etc.
? Logout removes ONLY admin session
```

### **Client Website:**
```
? Shows client email in dropdown
? Can browse products
? Can add to cart/wishlist
? Logout removes ONLY client session
```

### **Independence:**
```
? Admin login doesn't log into client
? Client login doesn't log into admin
? Admin logout doesn't log out client
? Client logout doesn't log out admin
? Both can be logged in simultaneously
```

---

## ?? **What To Look For**

### **? CORRECT Behavior:**
```
- Admin login ? Only admin cookie created
- Client login ? Only client cookie created
- Admin shows in admin panel ONLY
- Client shows on website ONLY
- Independent logout
```

### **? WRONG Behavior (Old Problem):**
```
- Admin login ? Shows on client website
- Client login ? Shows in admin panel
- One cookie for both
- Sessions overlap
- Logout affects both
```

---

## ?? **Quick Checklist**

- [ ] Admin login creates .MobileStore.Admin cookie
- [ ] Client login creates .MobileStore.Client cookie
- [ ] Admin cookie doesn't affect client site
- [ ] Client cookie doesn't affect admin panel
- [ ] Can be logged into both simultaneously
- [ ] Admin logout removes admin cookie only
- [ ] Client logout removes client cookie only
- [ ] Both sessions work independently

---

## ?? **SUCCESS!**

If all tests pass, your authentication is perfectly separated!

---

**See `SEPARATE_AUTHENTICATION_SOLUTION.md` for complete documentation.**

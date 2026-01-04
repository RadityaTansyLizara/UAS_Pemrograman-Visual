# Razor Error Fixed + SQLite Decimal Issue Resolved ✅

## Issues Fixed

### 1. Razor Tag Helper Error (RZ1031)
**Problem:**
```
error RZ1031: The tag helper 'option' must not have C# in the element's attribute declaration area
```

**Cause:**
Razor Tag Helper tidak bisa memiliki kode C# inline di dalam attribute menggunakan sintaks `@(...)`.

**Solution:**
Menggunakan conditional rendering dengan `@if/@else` blocks instead of inline expressions.

**Before (Error):**
```html
<option value="@category.Id" @(ViewBag.CurrentCategory == category.Id ? "selected" : "")>
```

**After (Fixed):**
```html
@if (ViewBag.CurrentCategory == category.Id)
{
    <option value="@category.Id" selected>@category.Name</option>
}
else
{
    <option value="@category.Id">@category.Name</option>
}
```

### 2. SQLite Decimal ORDER BY Error
**Problem:**
```
System.NotSupportedException: SQLite does not support expressions of type 'decimal' in ORDER BY clauses
```

**Cause:**
SQLite tidak support ORDER BY dengan tipe data `decimal` (Price field).

**Solution:**
Load data ke memory terlebih dahulu, kemudian sort menggunakan LINQ to Objects.

**Before (Error):**
```csharp
// Sort in database
query = sortBy switch
{
    "price_asc" => query.OrderBy(p => p.Price),
    "price_desc" => query.OrderByDescending(p => p.Price),
    ...
};
var products = await query.ToListAsync();
```

**After (Fixed):**
```csharp
// Get products first
var products = await query.ToListAsync();

// Sort in memory
products = sortBy switch
{
    "price_asc" => products.OrderBy(p => p.Price).ToList(),
    "price_desc" => products.OrderByDescending(p => p.Price).ToList(),
    ...
};
```

## Files Modified

### 1. Views/Product/Index.cshtml
- Fixed category dropdown selected attribute
- Fixed sort dropdown selected attribute
- Used conditional rendering instead of inline C# expressions

### 2. Controllers/ProductController.cs
- Changed sorting logic to use LINQ to Objects
- Load data first, then sort in memory
- Avoids SQLite decimal ORDER BY limitation

### 3. Program.cs
- Commented out `EnsureDeleted()` to prevent database lock issues
- Only use `EnsureCreated()` to ensure schema exists

## Build Status

✅ **Build Successful**
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

✅ **Application Running**
```
http://localhost:5055
```

✅ **No Runtime Errors**
- Database queries working
- Sorting working (in memory)
- No exceptions

## Testing

### Test Sort Functionality
1. Open: `http://localhost:5055/Product`
2. Select "Harga Terendah" from dropdown
3. **Result:** Products sorted by price (low to high) ✓
4. Select "Harga Tertinggi"
5. **Result:** Products sorted by price (high to low) ✓
6. Select "Nama A-Z"
7. **Result:** Products sorted alphabetically ✓

### Test Category + Sort
1. Select category "Pakaian Bayi"
2. Select sort "Harga Terendah"
3. **Result:** Only "Pakaian Bayi" products, sorted by price ✓

## Performance Note

**In-Memory Sorting:**
- Products are loaded into memory first
- Sorting happens in application layer (not database)
- Performance impact is minimal for small to medium datasets
- For large datasets (>10,000 products), consider:
  - Pagination
  - Caching
  - Converting Price to double/float type

## Alternative Solutions (Future)

### Option 1: Change Price Type
```csharp
// In Product.cs
public double Price { get; set; }  // Instead of decimal
```
Pros: Database sorting works
Cons: Less precision for currency

### Option 2: Use PostgreSQL/SQL Server
These databases support decimal in ORDER BY
Pros: Full database sorting support
Cons: More complex setup

### Option 3: Add Computed Column
```csharp
// Add PriceDouble column for sorting
public double PriceDouble => (double)Price;
```
Pros: Keep decimal precision
Cons: Extra column

## Current Solution

✅ **In-memory sorting is the best solution for now because:**
1. Simple to implement
2. No schema changes needed
3. Works with existing SQLite database
4. Performance is acceptable for typical product catalogs
5. No precision loss

## Status

✅ **ALL ERRORS FIXED**
✅ **APPLICATION RUNNING**
✅ **SORT FEATURE WORKING**
✅ **READY TO TEST**

---

**Test URL:** `http://localhost:5055/Product`

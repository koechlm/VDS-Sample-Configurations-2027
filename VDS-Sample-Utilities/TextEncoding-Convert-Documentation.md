# Documentation: `TextEncoding` and `Convert` Classes for PowerShell Consumption

This document describes the usage of the `TextEncoding` and `Convert` classes in the `VdsSampleUtilities` assembly, specifically for Vault Data Standard PowerShell scripts. Each method is documented with its purpose, parameters, return type, and PowerShell usage.

---

## 1. TextEncoding Class

Provides encoding and decoding methods for strings and byte arrays using UTF8 and ASCII.

### Instance Methods

#### `UTF8GetBytes(string String) : byte[]`
- **Purpose:** Converts a string to a UTF8-encoded byte array.
- **Parameters:**  
  - `String` (`string`): The input string to encode.
- **Returns:**  
  - `byte[]`: UTF8-encoded byte array.
- **PowerShell Usage:**
```powershell
$_TextEncoding = New-Object VdsSampleUtilities.TextEncoding
$bytes = $_TextEncoding.UTF8GetBytes("Hello World")
```

#### `ASCIIGetBytes(string String) : byte[]`
- **Purpose:** Converts a string to an ASCII-encoded byte array.
- **Parameters:**  
  - `String` (`string`): The input string to encode.
- **Returns:**  
  - `byte[]`: ASCII-encoded byte array.
- **PowerShell Usage:**
```powershell
$_TextEncoding = New-Object VdsSampleUtilities.TextEncoding
$bytes = $_TextEncoding.ASCIIGetBytes("Hello World")
```
#### `UTF8GetString(byte[] Bytes) : string`
- **Purpose:** Converts a UTF8-encoded byte array to a string.
- **Parameters:**  
  - `Bytes` (`byte[]`): The UTF8-encoded byte array to decode.
- **Returns:**  
  - `string`: The decoded string.
- **PowerShell Usage:**
```powershell
$_TextEncoding = New-Object VdsSampleUtilities.TextEncoding
$string = $_TextEncoding.UTF8GetString($bytes)
```
#### `ASCIIGetString(byte[] Bytes) : string`
- **Purpose:** Converts an ASCII-encoded byte array to a string.
- **Parameters:**  
  - `Bytes` (`byte[]`): The ASCII-encoded byte array to decode.
- **Returns:**  
  - `string`: The decoded string.
- **PowerShell Usage:**
```powershell
$_TextEncoding = New-Object VdsSampleUtilities.TextEncoding
$string = $_TextEncoding.ASCIIGetString($bytes)
```

### Static Methods

#### `GetUTF8Bytes(string String) : byte[]` [Static]
- **Purpose:** Converts a string to a UTF8-encoded byte array (static method - no instantiation required).
- **Parameters:**  
  - `String` (`string`): The input string to encode.
- **Returns:**  
  - `byte[]`: UTF8-encoded byte array.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$bytes = [VdsSampleUtilities.TextEncoding]::GetUTF8Bytes("Hello World")
```

#### `GetASCIIBytes(string String) : byte[]` [Static]
- **Purpose:** Converts a string to an ASCII-encoded byte array (static method - no instantiation required).
- **Parameters:**  
  - `String` (`string`): The input string to encode.
- **Returns:**  
  - `byte[]`: ASCII-encoded byte array.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$bytes = [VdsSampleUtilities.TextEncoding]::GetASCIIBytes("Hello World")
```

#### `GetUTF8String(byte[] Bytes) : string` [Static]
- **Purpose:** Converts a UTF8-encoded byte array to a string (static method - no instantiation required).
- **Parameters:**  
  - `Bytes` (`byte[]`): The UTF8-encoded byte array to decode.
- **Returns:**  
  - `string`: The decoded string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$string = [VdsSampleUtilities.TextEncoding]::GetUTF8String($bytes)
```

#### `GetASCIIString(byte[] Bytes) : string` [Static]
- **Purpose:** Converts an ASCII-encoded byte array to a string (static method - no instantiation required).
- **Parameters:**  
  - `Bytes` (`byte[]`): The ASCII-encoded byte array to decode.
- **Returns:**  
  - `string`: The decoded string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$string = [VdsSampleUtilities.TextEncoding]::GetASCIIString($bytes)
```

---

## 2. Convert Class

Provides conversion methods between strings, numbers, and Base64 encoding. All methods are static and can be called without instantiation.

### Static Methods

#### `ToInt32(string value) : int` [Static]
- **Purpose:** Converts a string to a 32-bit signed integer.
- **Parameters:**  
  - `value` (`string`): String to convert.
- **Returns:**  
  - `int`: Converted integer.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$intValue = [VdsSampleUtilities.Convert]::ToInt32("123")
```

#### `Int32ToString(int value) : string` [Static]
- **Purpose:** Converts a 32-bit signed integer to a string.
- **Parameters:**  
  - `value` (`int`): Integer to convert.
- **Returns:**  
  - `string`: Converted string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$stringValue = [VdsSampleUtilities.Convert]::Int32ToString(123)
```

#### `ToInt64(string value) : long` [Static]
- **Purpose:** Converts a string to a 64-bit signed integer.
- **Parameters:**  
  - `value` (`string`): String to convert.
- **Returns:**  
  - `long`: Converted long integer.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$longValue = [VdsSampleUtilities.Convert]::ToInt64("1234567890123456789")
```

#### `Int64ToString(long value) : string` [Static]
- **Purpose:** Converts a 64-bit signed integer to a string.
- **Parameters:**  
  - `value` (`long`): Long integer to convert.
- **Returns:**  
  - `string`: Converted string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$stringValue = [VdsSampleUtilities.Convert]::Int64ToString(1234567890123456789)
```

#### `ToDouble(string value) : double` [Static]
- **Purpose:** Converts a string to a double-precision floating-point number.
- **Parameters:**  
  - `value` (`string`): String to convert.
- **Returns:**  
  - `double`: Converted double.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$doubleValue = [VdsSampleUtilities.Convert]::ToDouble("123.45")
```

#### `DoubleToString(double value) : string` [Static]
- **Purpose:** Converts a double-precision floating-point number to a string.
- **Parameters:**  
  - `value` (`double`): Double to convert.
- **Returns:**  
  - `string`: Converted string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$stringValue = [VdsSampleUtilities.Convert]::DoubleToString(123.45)
```

#### `ToBase64String(byte[] byteArray) : string` [Static]
- **Purpose:** Converts a byte array to a Base64-encoded string.
- **Parameters:**  
  - `byteArray` (`byte[]`): Byte array to encode.
- **Returns:**  
  - `string`: Base64 string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$base64String = [VdsSampleUtilities.Convert]::ToBase64String($byteArray)
```

#### `FromBase64String(string s) : byte[]` [Static]
- **Purpose:** Converts a Base64-encoded string to a byte array.
- **Parameters:**  
  - `s` (`string`): Base64 string to decode.
- **Returns:**  
  - `byte[]`: Decoded byte array.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$byteArray = [VdsSampleUtilities.Convert]::FromBase64String($base64String)
```

#### `StringToInt32(string value) : int` [Static]
- **Purpose:** Converts a string to a 32-bit signed integer (alternative naming for PowerShell compatibility).
- **Parameters:**  
  - `value` (`string`): String to convert.
- **Returns:**  
  - `int`: Converted integer.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$intValue = [VdsSampleUtilities.Convert]::StringToInt32("123")
```

#### `Int32ToStringStatic(int value) : string` [Static]
- **Purpose:** Converts a 32-bit signed integer to a string (static version).
- **Parameters:**  
  - `value` (`int`): Integer to convert.
- **Returns:**  
  - `string`: Converted string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$stringValue = [VdsSampleUtilities.Convert]::Int32ToStringStatic(123)
```

#### `StringToInt64(string value) : long` [Static]
- **Purpose:** Converts a string to a 64-bit signed integer (alternative naming for PowerShell compatibility).
- **Parameters:**  
  - `value` (`string`): String to convert.
- **Returns:**  
  - `long`: Converted long integer.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$longValue = [VdsSampleUtilities.Convert]::StringToInt64("1234567890123456789")
```

#### `Int64ToStringStatic(long value) : string` [Static]
- **Purpose:** Converts a 64-bit signed integer to a string (static version).
- **Parameters:**  
  - `value` (`long`): Long integer to convert.
- **Returns:**  
  - `string`: Converted string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$stringValue = [VdsSampleUtilities.Convert]::Int64ToStringStatic(1234567890123456789)
```

#### `StringToDouble(string value) : double` [Static]
- **Purpose:** Converts a string to a double-precision floating-point number (alternative naming for PowerShell compatibility).
- **Parameters:**  
  - `value` (`string`): String to convert.
- **Returns:**  
  - `double`: Converted double.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$doubleValue = [VdsSampleUtilities.Convert]::StringToDouble("123.45")
```

#### `DoubleToStringStatic(double value) : string` [Static]
- **Purpose:** Converts a double-precision floating-point number to a string (static version).
- **Parameters:**  
  - `value` (`double`): Double to convert.
- **Returns:**  
  - `string`: Converted string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$stringValue = [VdsSampleUtilities.Convert]::DoubleToStringStatic(123.45)
```

#### `BytesToBase64(byte[] byteArray) : string` [Static]
- **Purpose:** Converts a byte array to a Base64-encoded string (alternative naming for PowerShell compatibility).
- **Parameters:**  
  - `byteArray` (`byte[]`): Byte array to encode.
- **Returns:**  
  - `string`: Base64 string.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$base64String = [VdsSampleUtilities.Convert]::BytesToBase64($byteArray)
```

#### `Base64ToBytes(string s) : byte[]` [Static]
- **Purpose:** Converts a Base64-encoded string to a byte array (alternative naming for PowerShell compatibility).
- **Parameters:**  
  - `s` (`string`): Base64 string to decode.
- **Returns:**  
  - `byte[]`: Decoded byte array.
- **PowerShell Usage:**
```powershell
# No instantiation needed
$byteArray = [VdsSampleUtilities.Convert]::Base64ToBytes($base64String)
```
 
---

## Full PowerShell Sample

### Using Instance Methods (TextEncoding Only)

```powershell
# Create instance for TextEncoding
$_TextEncoding = New-Object VdsSampleUtilities.TextEncoding

# Sample text
$text = "Hello World"

# 1. TextEncoding Instance Usage
# Convert text to UTF8 and ASCII byte arrays
$utf8Bytes = $_TextEncoding.UTF8GetBytes($text)
$asciiBytes = $_TextEncoding.ASCIIGetBytes($text)

# Convert byte arrays back to text
$decodedUtf8Text = $_TextEncoding.UTF8GetString($utf8Bytes)
$decodedAsciiText = $_TextEncoding.ASCIIGetString($asciiBytes)

# 2. Convert Static Usage (No instantiation needed)
# Convert string to Int32, Int64, and Double
$intValue = [VdsSampleUtilities.Convert]::ToInt32("123")
$longValue = [VdsSampleUtilities.Convert]::ToInt64("1234567890123456789")
$doubleValue = [VdsSampleUtilities.Convert]::ToDouble("123.45")

# Convert Int32, Int64, and Double back to string
$intString = [VdsSampleUtilities.Convert]::Int32ToString($intValue)
$longString = [VdsSampleUtilities.Convert]::Int64ToString($longValue)
$doubleString = [VdsSampleUtilities.Convert]::DoubleToString($doubleValue)

# Convert byte array to Base64 string and back
$base64String = [VdsSampleUtilities.Convert]::ToBase64String($utf8Bytes)
$decodedBytes = [VdsSampleUtilities.Convert]::FromBase64String($base64String)
```

### Using Static Methods (Recommended)

```powershell
# Sample text
$text = "Hello World"

# 1. TextEncoding Static Usage (No instantiation required)
# Convert text to UTF8 and ASCII byte arrays
$utf8Bytes = [VdsSampleUtilities.TextEncoding]::GetUTF8Bytes($text)
$asciiBytes = [VdsSampleUtilities.TextEncoding]::GetASCIIBytes($text)

# Convert byte arrays back to text
$decodedUtf8Text = [VdsSampleUtilities.TextEncoding]::GetUTF8String($utf8Bytes)
$decodedAsciiText = [VdsSampleUtilities.TextEncoding]::GetASCIIString($asciiBytes)

# 2. Convert Static Usage (No instantiation required)
# Convert string to Int32, Int64, and Double
$intValue = [VdsSampleUtilities.Convert]::StringToInt32("123")
$longValue = [VdsSampleUtilities.Convert]::StringToInt64("1234567890123456789")
$doubleValue = [VdsSampleUtilities.Convert]::StringToDouble("123.45")

# Convert Int32, Int64, and Double back to string
$intString = [VdsSampleUtilities.Convert]::Int32ToStringStatic($intValue)
$longString = [VdsSampleUtilities.Convert]::Int64ToStringStatic($longValue)
$doubleString = [VdsSampleUtilities.Convert]::DoubleToStringStatic($doubleValue)

# Convert byte array to Base64 string and back
$base64String = [VdsSampleUtilities.Convert]::BytesToBase64($utf8Bytes)
$decodedBytes = [VdsSampleUtilities.Convert]::Base64ToBytes($base64String)
```

### Mixed Usage Example

```powershell
# Combine static methods from both classes
$text = "Hello World"

# Use static methods for one-off conversions
$utf8Bytes = [VdsSampleUtilities.TextEncoding]::GetUTF8Bytes($text)
$base64String = [VdsSampleUtilities.Convert]::BytesToBase64($utf8Bytes)

# Display result
Write-Host "Original: $text"
Write-Host "Base64: $base64String"

# Decode back using static methods
$decodedBytes = [VdsSampleUtilities.Convert]::Base64ToBytes($base64String)
$decodedText = [VdsSampleUtilities.TextEncoding]::GetUTF8String($decodedBytes)
Write-Host "Decoded: $decodedText"

# Number conversions
$numberString = "42"
$number = [VdsSampleUtilities.Convert]::ToInt32($numberString)
$doubleNumber = [VdsSampleUtilities.Convert]::ToDouble("3.14159")
Write-Host "Number: $number"
Write-Host "Pi: $doubleNumber"
```
---

## Notes

- Always ensure the assembly is loaded with `Add-Type -Path ...` before using these classes.
- All methods throw exceptions for invalid input (null or empty).
- **TextEncoding** offers both instance methods and static methods for convenience:
  - Use **instance methods** when you need to create an object once and reuse it multiple times
  - Use **static methods** for one-off conversions without instantiation
  - Static methods are ideal for inline operations and reduce object creation overhead
- **Convert** class now uses **static methods only** for all conversions:
  - No instantiation required - all methods can be called directly on the class
  - Provides two naming conventions for better PowerShell compatibility:
    - Standard: `ToInt32()`, `ToInt64()`, `ToDouble()`, `ToBase64String()`, etc.
    - Alternative: `StringToInt32()`, `StringToInt64()`, `BytesToBase64()`, etc.
- **Method Naming Conventions:**
  - **TextEncoding** static methods: `GetUTF8Bytes()`, `GetUTF8String()`, `GetASCIIBytes()`, `GetASCIIString()`
  - **Convert** static methods: Both standard (`ToInt32()`, `Int32ToString()`) and alternative naming (`StringToInt32()`, `Int32ToStringStatic()`)



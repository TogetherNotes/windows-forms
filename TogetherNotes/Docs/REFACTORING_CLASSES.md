# 🚀 Refactoring Guide: Renaming or Deleting a Class in TogetherNotes

This guide explains how to **rename or delete a class** in the **TogetherNotes** project without causing errors.

---

## 🛠️ Renaming an Existing Class

### 📌 1️⃣ Rename the Class in `Forms/`
1. Open the `Forms/` folder and **rename the class** to the new desired name.
2. Open the corresponding `.xaml` file and **modify the first line** to match the new name.

   **Example:**
   ```xml
   <UserControl x:Class="TogetherNotes.Forms.Shipment"
   ```
   ➡️ Change to:
   ```xml
   <UserControl x:Class="TogetherNotes.Forms.Faqs"
   ```

---

### 📌 2️⃣ Update the Associated ViewModel
1. **Rename the `ViewModel`** to match the new class name.
   - Example: `ShipmentVM` ➡️ `FaqsVM`
2. **Update the class declaration and methods**:
   ```csharp
   public class FaqsVM : ViewModelBase
   ```
   Ensure all methods and properties within the `ViewModel` reference the new class name.

---

### 📌 3️⃣ Update the `DataTemplate.xaml` in `Utils/`
1. Open `Utils/DataTemplate.xaml`.
2. Locate the old view reference and update it with the new class name.
   
   **Example:**
   ```xml
   <DataTemplate DataType="{x:Type vm:ShipmentVM}">
       <local:Shipment />
   </DataTemplate>
   ```
   ➡️ Change to:
   ```xml
   <DataTemplate DataType="{x:Type vm:FaqsVM}">
       <local:Faqs />
   </DataTemplate>
   ```

---

### 📌 4️⃣ Update `NavigationVM.cs` in `ViewModel/`
1. Open `ViewModel/NavigationVM.cs`.
2. Locate the method where the old class was referenced and update it.

   **Example:**
   ```csharp
   private void Shipment(object obj)
   {
       if (IsAuthenticated)
           CurrentView = new ShipmentVM();
   }
   ```
   ➡️ Change to:
   ```csharp
   private void Faqs(object obj)
   {
       if (IsAuthenticated)
           CurrentView = new FaqsVM();
   }
   ```

---

## ❌ Deleting a Class
If you need to remove a class, follow these steps:

1. **Delete the class** from the `Forms/` folder.
2. **Remove the corresponding ViewModel** from the `ViewModel/` folder.
3. **Delete the entry in `DataTemplate.xaml`** in `Utils/`.
4. **Remove the reference** in `NavigationVM.cs` in `ViewModel/`.

---

Following these steps ensures a smooth refactoring process while maintaining the integrity of the project.
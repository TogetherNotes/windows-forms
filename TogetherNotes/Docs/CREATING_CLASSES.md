# 🚀 Guide to Creating a New Class in TogetherNotes

This guide explains how to create a new class in the TogetherNotes project, step by step, ensuring everything is properly wired up.

---

## 🛠️ Creating a New Class

### 📌 1️⃣ Add the New Class in Forms/

1. Navigate to the `Forms/` folder.

2. Create a new class file and a corresponding XAML file.

   **Example:**
    - New class: `Faqs.xaml.cs`
    - New XAML file: `Faqs.xaml`


3. Define your class and link it in the XAML file:

    ```xml
   <UserControl x:Class="TogetherNotes.Forms.Faqs"
   ```

---

### 📌 2️⃣ Create the Associated ViewModel

1. In the `ViewModel/` folder, **add a new ViewModel class**.

   **Example:** `FaqsVM.cs`

2. Inherit from `ViewModelBase` and define necessary properties and methods:

   ```csharp
   public class FaqsVM : ViewModelBase
   {
       public FaqsVM()
       {
           // Initialize properties
       }
   }
   ```

---

### 📌 3️⃣ Register the New Class in `DataTemplate.xaml`

1. Open `Utils/DataTemplate.xaml`.
2. **Add a new DataTemplate** for the class and its ViewModel:
   ```xml
   <DataTemplate DataType="{x:Type vm:FaqsVM}">
       <local:Faqs />
   </DataTemplate>
   ```

---

### 📌 4️⃣ Add Navigation Logic in `NavigationVM.cs`

1. Open `ViewModel/NavigationVM.cs`.

2. **Add a method to handle the new class’s navigation:**

   ```csharp
   private void Faqs(object obj)
   {
       if (IsAuthenticated)
           CurrentView = new FaqsVM();
   }
   ```

3. **Hook up the navigation method** in the relevant command or menu item.

---

## ✅ Final Steps

- **Test the navigation** to ensure the new class loads correctly.
- **Verify bindings** and **UI interactions** work as expected.
- **Clean and rebuild the solution** to catch any potential issues.

By following these steps, you’ll seamlessly integrate a new class into the TogetherNotes project! 

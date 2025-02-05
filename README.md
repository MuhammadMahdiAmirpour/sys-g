# sys-g - Task Scheduler System

A C# implementation of a task scheduling system that uses heap data structure for efficient task management. This project was developed as part of System Group's homework assignment 2.

## 🚀 Features

- **Task Management**: Efficient handling of tasks with priorities and due dates
- **Heap Implementation**: Custom heap data structure with both min-heap and max-heap capabilities
- **MongoDB Integration**: Task persistence using MongoDB database
- **Flexible Task Scheduling**: Toggle between heap-based and LINQ-based task scheduling
- **Priority Management**: Support for different priority levels in task scheduling

## 🏗️ Project Structure

The project consists of two main components:

1. **Heap Implementation (`sys-g-heap`)**
   - Generic heap data structure
   - Support for custom comparers
   - Basic heap operations (Insert, ExtractRoot, PeekRoot, etc.)

2. **Task Scheduler (`sys-scheduler`)**
   - Task management system
   - MongoDB integration for persistence
   - Priority-based task scheduling
   - Switchable scheduling algorithms

## 🛠️ Technical Stack

- **Language**: C# (.NET Core)
- **Database**: MongoDB
- **Architecture**: Object-Oriented Design with Interface-based Programming
- **Data Structures**: Custom Heap Implementation

## 📋 Prerequisites

- .NET Core SDK (Latest Version)
- MongoDB Server
- IDE (Visual Studio, Rider, or VS Code)

## 🚀 Getting Started

1. Clone the repository:
```bash
git clone https://github.com/MuhammadMahdiAmirpour/sys-g.git
```

2. Navigate to the project directory:
```bash
cd sys-g
```

3. Restore dependencies:
```bash
dotnet restore
```

4. Configure MongoDB connection string in the application settings.

5. Run the application:
```bash
dotnet run
```

## 🧮 Data Structures

### Heap Implementation
- Generic heap structure supporting both min and max heaps
- Custom comparers for flexible sorting
- O(log n) complexity for insert and extract operations

### Task Structure
- Title
- Description
- Creation Date
- Due Date
- Finish Date
- Priority Level

## 📊 Task Priority Levels

Tasks can be assigned one of the following priority levels:
- High
- Medium
- Low

## 🔄 Usage

The system provides two ways to manage tasks:
1. **Heap-based**: Efficient for getting the closest task by due date
2. **LINQ-based**: Alternative approach using database queries

Toggle between these approaches using the `ToggleHeapUsage()` method.

## 📝 Example Usage

```csharp
var scheduler = new TaskScheduler(connectionString, databaseName, collectionName);

// Toggle between heap and LINQ-based approaches
scheduler.ToggleHeapUsage();

// Get the closest task to current date
var nextTask = scheduler.GetClosestTask(DateTime.Now);

// Get statistics
var totalTasks = scheduler.GetNumberOfTasks();
var finishedTasks = scheduler.GetNumberOfFinishedTasks();
var unfinishedTasks = scheduler.GetNumberOfUnfinishedTasks();
```

## 🤝 Contributing

Contributions, issues, and feature requests are welcome. Feel free to check [issues page](https://github.com/MuhammadMahdiAmirpour/sys-g/issues) if you want to contribute.

## 📜 License

This project is part of a homework assignment for System Group. All rights reserved.

## ✍️ Author

**Muhammad Mahdi Amirpour**
- GitHub: [@MuhammadMahdiAmirpour](https://github.com/MuhammadMahdiAmirpour)

## 🔍 Additional Notes

- The project uses MongoDB for persistence
- Includes dummy data generation for testing
- Implements efficient task prioritization algorithms
```

This README provides a comprehensive overview of your project, including:
- Clear project description
- Features and technical stack
- Setup instructions
- Usage examples
- Project structure
- Implementation details
- Contributing guidelines


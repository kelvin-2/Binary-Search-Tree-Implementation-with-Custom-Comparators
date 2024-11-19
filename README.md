# Binary Search Tree Implementation with Custom Comparators  

This project demonstrates a **Binary Search Tree (BST)** implementation that uses custom comparators to organize data dynamically. The BST is designed to work with `Person` objects, where the order of nodes can be customized using an `IComparer` implementation.  

## Features  

- **Customizable Ordering**  
  - Implemented a comparator to order `Person` objects alphabetically by their names.  
- **Dynamic Searching**  
  - Search functionality allows querying the tree for specific `Person` objects using dummy instances, e.g., searching for a person by age.  
- **File Integration**  
  - Reads input from a text file where each line defines a `Person` object (name, age, salary).  
  - Outputs the names of all persons in the tree in alphabetical order to an `Outputs.txt` file.  
- **Inorder Traversal**  
  - Retrieves and displays all names in the tree in sorted order using the `getInOrder()` method.  

## Input File Format  

Each line in the input file represents a single `Person` object with values separated by commas. A `<end>` line indicates the end of input.  

### Example Input File:  
```plaintext
Imka,18,12441.10  
Markus,25,23894.50  
Kaya,22,18004.30  
<end>

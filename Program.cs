using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BST_Person_Name_Sorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }
        Program() 
        {
            BSTPerson person = new BSTPerson(new PersonNameComparer());
        }
    }
    public class Person
    {
        public String name;
        public int age;
        public double salary;

        public Person(String n, int a, double s)
        {
            name = n;
            age = a;
            salary = s;
        }
    }
    class BSTPerson
    {
        private BTNode root = null;
        private IComparer comparator;
        public BSTPerson(IComparer comparator)
        {
            this.comparator = comparator;
        }
        //set the root to null to empty the tree
        public void clear()
        {
            root = null;
        }
        void ReadData()
        {
            string name;
            int age;
            double salary;

            string[] fields;
            StreamReader sr = new StreamReader("TestInputs.txt");
            string line = sr.ReadLine();
            while (line != "<end>")
            {
                fields = line.Split(',');
                name = fields[0];
                age = int.Parse(fields[1]);
                salary = double.Parse(fields[2]);

                Person person = new Person(name, age, salary);
                insert(person);
                line = sr.ReadLine();
            }
            sr.Close();
        }
        public void insert(Person insertedValue)
        {
            if (root == null)
            {
                root = new BTNode(insertedValue);
            }
            else
            {
                BTNode cur = root;
                bool inserted = false;
                while (!inserted)
                {
                    if (comparator.Compare(cur.value, inserted) > 0)
                    {
                        if (cur.left == null)
                        {
                            cur.left = new BTNode(insertedValue);
                            inserted = true;
                        }
                        else cur = cur.left;
                    }
                    else
                    {
                        if (cur.right == null)
                        {
                            cur.right = new BTNode(insertedValue);
                            inserted = true;
                        }
                        else cur = cur.right;
                    }
                }
            }

        }
        public string FindRage(Person being, Person End)
        {
            return FindRangeHelper(root, being, End);
        }
        private string FindRangeHelper(BTNode cur, Person beign, Person End)
        {
            //we want to find the elementst that are in between 2 values 
            if (cur == null) { return ""; }
            //if the elements are less than what is in the code 
            if (comparator.Compare(cur.value, beign) < 0)
            {
                return FindRangeHelper(cur.right, beign, End);
            }
            //if its greater than the last element then its not its not in range
            else if (comparator.Compare(cur.value, End) < 0)
            {
                return FindRangeHelper(cur.left, beign, End);
            }
            else return FindRangeHelper(cur.left, beign, End) + cur.value.name + "," + FindRangeHelper(cur.right, beign, End);

        }
        public Person delete(Person deleteValue) //For this example the person to be deleted is returned
        {
            if (root == null) return null;

            BTNode cur = root;
            BTNode parentOfCur = root;
            while (cur != null)
            {
                if (comparator.Compare(cur.value, deleteValue) == 0)
                {
                    Person returnValue = cur.value;

                    if ((cur.left == null) && (cur.right == null))  //Check whether this is a leaf node
                    {
                        if (cur == root) root = null;
                        else                                        //Either the left of right of the parent of cur must be set to null
                        {
                            if (cur == parentOfCur.left) parentOfCur.left = null;
                            else parentOfCur.right = null;
                        }
                    }
                    else // cur is an internal node
                    {
                        if (cur.right == null) // then bring up left subtree
                        {
                            if (cur == root) root = cur.left;
                            else
                            {
                                //change of the pointers 
                                if (cur == parentOfCur.left) parentOfCur.left = cur.left;
                                else parentOfCur.right = cur.left;
                            }
                        }
                        else if (cur.left == null) // then bring up the right subtree
                        {
                            if (cur == root) root = cur.right;
                            else
                            {
                                if (cur == parentOfCur.left) parentOfCur.left = cur.right;
                                else parentOfCur.right = cur.right;
                            }
                        }
                        else //both left and right are not null. Replace cur with in-order successor
                        {
                            cur.value = removeSuccessor(cur);
                        }
                    }
                    return returnValue;
                }
                else
                {
                    parentOfCur = cur;
                    if (comparator.Compare(cur.value, deleteValue) > 0) cur = cur.left;    //deleteValue is in the left subtree
                    else cur = cur.right;                           //deleteValue is in the right subtree
                }
            }
            return null;  //cur is null so deleteValue is not in the tree
        }
        private Person removeSuccessor(BTNode T)
        {
            BTNode parentOfSucc = T;
            BTNode successor = T.right; // Subtree contains those values > T. We know T.right will not be null because T has two children
            if (successor.left == null)  //The successor is the immediate node to the right of T
            {
                parentOfSucc.right = successor.right;
                return successor.value;
            }
            while (successor.left != null)  //Go down as far as we can to the left
            {
                parentOfSucc = successor;
                successor = successor.left;
            }
            parentOfSucc.left = successor.right; //Reached a node that has no left child. Remove this node

            return successor.value;
        }
        public String getInOrder()
        {
            return inOrder(root);
        }


        private String inOrder(BTNode cur)
        {
            if (cur == null) return "";
            else return inOrder(cur.left) + cur.value.name + "," + inOrder(cur.right);
        }

        private String preOrder(BTNode cur)
        {
            if (cur == null) return "";
            else return cur.value.name + "," + preOrder(cur.left) + preOrder(cur.right);
        }

        public String getPreOrder()
        {
            return preOrder(root);
        }


    }
    class BTNode
    {
        public BTNode left, right;
        public Person value;

        public BTNode(Person v)
        {
              value = v;
        }
    }
    class PersonNameComparer : IComparer
    {
        public int Compare(object x,object y)
        {
            Person personx=x as Person;
            Person persony=y as Person;
            return string.Compare(personx.name, persony.name);
        }
    }


}

#include <stdio.h>
#include <stdlib.h>

#define TRUE 1
#define FALSE 0

#define NODE_DEGREE 2
#define MAX_CHILDS (NODE_DEGREE * 2)
#define MAX_KEYS (MAX_CHILDS - 1)

#define SPACE (4 * MAX_KEYS)

typedef struct BTreeNode
{
    int Keys[MAX_KEYS];
    struct BTreeNode *Childs[MAX_CHILDS];
    int KeyIndex;
    int Leaf;
} BTreeNode;

typedef struct
{
    struct BTreeNode *Root;
    int Degree;
} BTree;

BTreeNode *CreateBTreeNode();
BTree *CreateBTree();

void Search(BTreeNode *Node, int Key);

void SplitChild(BTreeNode *Parent, BTreeNode *Child, int Index);

void InsertNonFull(BTreeNode *Node, int Key);
void Insert(BTree *Tree, int Key);

void Remove(BTree *Tree, int Key);

int FindKey(BTreeNode *Node, int Key);
void RemoveNode(BTreeNode *Node, int Key);

void RemoveFromLeaf(BTreeNode *Node, int Index);
void RemoveFromNoLeaf(BTreeNode *Node,  int Index);

void Fill(BTreeNode *Node,  int Index);

int RetPredecessor(BTreeNode *Node, int Index);
int RetSuccessor(BTreeNode *Node, int Index);

void BorrowFromPrev(BTreeNode *Node, int Index);
void BorrowFromNext(BTreeNode *Node, int Index);

void Merge(BTreeNode *Node, int Index);

void PrintTree(BTreeNode *Node, int Level, int Blank);

int main(int argc, char *argv[])
{
    int test[] = {1, 7, 2, 11, 4, 8, 13, 10, 5, 19, 3, 6, 9, 18, 23, 12, 14, 20, 21, 16, 26, 27, 22, 24, 25};

    BTree *Tree = CreateBTree();
    int i;

    for(i = 0; i < sizeof(test)/sizeof(int); i++)
    {
        printf("Insert : %d\n", test[i]);
        Insert(Tree, test[i]);
        PrintTree(Tree->Root, 0, FALSE);
        printf("\n");
    }

    for(int i = 0; i < 3 * (SPACE + 1); i++)
    {
        printf("-");
    }
    printf("\n");

    int testdel[] = {14, 3, 12, 27, 1, 10, 11, 25, 5, 9, 23, 20, 8, 4, 24, 1};

    for(int i = 0; i < sizeof(testdel)/sizeof(int); i++)
    {
        printf("Remove : %d\n", testdel[i]);
        Remove(Tree, testdel[i]);
        PrintTree(Tree->Root, 0, FALSE);
        printf("\n");
    }

    for(int i = 0; i < 3 * (SPACE + 1); i++)
    {
        printf("-");
    }
    printf("\n");
    
    int Target = 2;
    printf("Search %d\n", Target);
    Search(Tree->Root, Target);
    printf("\n");

    Target = 26;
    printf("Search %d\n", Target);
    Search(Tree->Root, Target);
    printf("\n");

    Target = 18;
    printf("Search %d\n", Target);
    Search(Tree->Root, Target);
    printf("\n");

    Target = 14;
    printf("Search %d\n", Target);
    Search(Tree->Root, Target);
    printf("\n");
    
    return 0;
}

BTreeNode *CreateBTreeNode()
{
    BTreeNode *temp = (BTreeNode*)malloc(sizeof(BTreeNode));

    for(int i = 0; i < MAX_KEYS; i++)
    {
        temp->Keys[i] = 0;
    }

    for(int i = 0; i < MAX_CHILDS; i++)
    {
        temp->Childs[i] = NULL;
    }

    temp->KeyIndex = 0;
    temp->Leaf = TRUE;

    return temp;
}

BTree *CreateBTree()
{
    BTree *Tree = (BTree*)malloc(sizeof(BTree));

    BTreeNode *temp = CreateBTreeNode();

    Tree->Degree = NODE_DEGREE;
    Tree->Root = temp;

    return Tree;
}

void Search(BTreeNode *Node, int Key)
{
    for(int i = 0; i < Node->KeyIndex; i++)
    {
        if(Node->Leaf == FALSE && Key < Node->Keys[i])
        {
            Search(Node->Childs[i], Key);
            return;
        }
        else if(Key == Node->Keys[i])
        {
            printf("Key %d exist in tree", Key);
            return;
        }
    }
    if(Node->Leaf == FALSE)
    {
        Search(Node->Childs[Node->KeyIndex], Key);
    }
    else
    {
        printf("Key %d not exist in tree", Key);
    }
}

void SplitChild(BTreeNode *Parent, BTreeNode *Child, int Index)
{
    BTreeNode *temp = CreateBTreeNode();

    temp->Leaf = Child->Leaf;
    temp->KeyIndex = NODE_DEGREE - 1;

    for(int i = 0; i < NODE_DEGREE - 1; i++) 
    {
        temp->Keys[i] = Child->Keys[NODE_DEGREE + i];
    }

    if(Child->Leaf == FALSE)
    {
        for(int i = 0; i < NODE_DEGREE; i++)
        {
            temp->Childs[i] = Child->Childs[NODE_DEGREE + i];
        }
    }
    Child->KeyIndex = NODE_DEGREE - 1;

    for(int i = Parent->KeyIndex; i > Index - 1; i--)
    {
        Parent->Childs[i + 1] = Parent-> Childs[i];
    }

    Parent->Childs[Index] = temp;

    for(int i = Parent->KeyIndex; i> Index - 1; i--)
    {
        Parent->Keys[i] = Parent->Keys[i - 1];
    }
    Parent->Keys[Index - 1] = Child->Keys[NODE_DEGREE - 1];

    Parent->KeyIndex++;
}

void InsertNonFull(BTreeNode *Node, int Key)
{
    int Index = Node->KeyIndex;

    if(Node->Leaf == TRUE)
    {
        while(Index > 0 && Node->Keys[Index - 1] > Key)
        {
            Node->Keys[Index] = Node->Keys[Index - 1];
            Index--;
        }
        Node->Keys[Index] = Key;
        Node->KeyIndex++;
    }
    else
    {
        while(Index > 0 && Node->Keys[Index - 1] > Key)
        {
            Index--;
        }
        
        if(Node->Childs[Index]->KeyIndex == MAX_KEYS)
        {
            SplitChild(Node, Node->Childs[Index], Index + 1);

            if(Node->Keys[Index] < Key)
            {
                Index++;
            }
        }
        InsertNonFull(Node->Childs[Index], Key);
    }
    
}

void Insert(BTree *Tree, int Key)
{
    if(Tree->Root->KeyIndex == MAX_KEYS)
    {
        BTreeNode *temp = CreateBTreeNode();

        temp->Leaf = FALSE;
        temp->KeyIndex = 0;
        temp->Childs[0] = Tree->Root;

        SplitChild(temp, Tree->Root, 1);

        InsertNonFull(temp, Key);

        Tree->Root = temp;
    }
    else
    {
        InsertNonFull(Tree->Root, Key);
    }
}

void Remove(BTree *Tree, int Key)
{
    RemoveNode(Tree->Root, Key);

    if(Tree->Root->KeyIndex == 0)
    {
        if(Tree->Root->Leaf == TRUE)
        {
            Tree->Root = CreateBTreeNode();
        }
        else
        {
            Tree->Root = Tree->Root->Childs[0];
        }
    }
}

int FindKey(BTreeNode *Node, int Key)
{
    int Index = 0;
    while(Index < Node->KeyIndex && Node->Keys[Index] < Key)
    {
        Index++;
    }

    return Index;
}

void RemoveNode(BTreeNode *Node, int Key)
{
    int Index = FindKey(Node, Key);

    if(Index < Node->KeyIndex && Node->Keys[Index] == Key)
    {
        if(Node->Leaf == TRUE)
        {
            RemoveFromLeaf(Node, Index);
        }
        else
        {
            RemoveFromNoLeaf(Node, Index);
        }
        
    }
    else
    {
        if(Node->Leaf == TRUE)
        {
            printf("Key %d is not exist in the tree\n", Key);
            return;
        }

        int Flag;
        if(Node->KeyIndex == Index)
        {
            Flag = TRUE;
        }
        else
        {
            Flag = FALSE;
        }
        
        if(Node->Childs[Index]->KeyIndex < NODE_DEGREE)
        {
            Fill(Node, Index);
        }

        if(Flag && (Index > Node->KeyIndex))
        {
            printf("Flaged!!\n");
            RemoveNode(Node->Childs[Index - 1], Key);
        }
        else
        {
            RemoveNode(Node->Childs[Index], Key);
        }
    }    
}

void RemoveFromLeaf(BTreeNode *Node, int Index)
{
    for(int i = Index + 1; i < Node->KeyIndex; i++)
    {
        Node->Keys[i - 1] = Node->Keys[i];
    }

    Node->KeyIndex--;
}

void RemoveFromNoLeaf(BTreeNode *Node,  int Index)
{
    int Key = Node->Keys[Index];

    if(Node->Childs[Index]->KeyIndex > NODE_DEGREE - 1)
    {
        int Pred = RetPredecessor(Node, Index);
        Node->Keys[Index] = Pred;
        RemoveNode(Node->Childs[Index], Pred);
    }
    else if(Node->Childs[Index + 1]->KeyIndex > NODE_DEGREE - 1)
    {
        int Succ = RetSuccessor(Node, Index);
        Node->Keys[Index] = Succ;
        RemoveNode(Node->Childs[Index + 1], Succ);
    }
    else
    {
        Merge(Node, Index);
        RemoveNode(Node->Childs[Index], Key);
    } 
}

void Fill(BTreeNode *Node,  int Index)
{
    if(Index != 0 && Node->Childs[Index - 1]->KeyIndex > NODE_DEGREE - 1)
    {
        BorrowFromPrev(Node, Index);
    }
    else if(Index != Node->KeyIndex && Node->Childs[Index + 1]->KeyIndex > NODE_DEGREE - 1)
    {
        BorrowFromNext(Node, Index);
    }
    else
    {
        if(Index != Node->KeyIndex)
        {
            Merge(Node, Index);
        }
        else
        {
            Merge(Node, Index - 1);
        }        
    }
}

int RetPredecessor(BTreeNode *Node, int Index)
{
    BTreeNode *Curr = Node->Childs[Index];
    while (Curr->Leaf == FALSE)
    {
        Curr = Curr->Childs[Curr->KeyIndex];
    }

    return Curr->Keys[Curr->KeyIndex - 1];
}

int RetSuccessor(BTreeNode *Node, int Index)
{
    BTreeNode *Curr = Node->Childs[Index + 1];
    while (Curr->Leaf == FALSE)
    {
        Curr = Curr->Childs[0];
    }
    
    return Curr->Keys[0];
}

void BorrowFromPrev(BTreeNode *Node, int Index)
{
    BTreeNode *Child = Node->Childs[Index];
    BTreeNode *Sibling = Node->Childs[Index - 1];

    for(int i = Child->KeyIndex - 1; i > -1; i--)
    {
        Child->Keys[i + 1] = Child->Keys[i];
    }

    if(Child->Leaf == FALSE)
    {
        for(int i = Child->KeyIndex; i > -1; i--)
        {
            Child->Childs[i + 1] = Child->Childs[i];
        }
    }
    Child->Keys[0] = Node->Keys[Index - 1];

    if(Child->Leaf == FALSE)
    {
        Child->Childs[0] = Sibling->Childs[Sibling->KeyIndex];
    }
    Node->Keys[Index - 1] = Sibling->Keys[Sibling->KeyIndex - 1];

    Child->KeyIndex++;
    Sibling->KeyIndex--;
}

void BorrowFromNext(BTreeNode *Node, int Index)
{
    BTreeNode *Child = Node->Childs[Index];
    BTreeNode *Sibling = Node->Childs[Index + 1];

    Child->Keys[Child->KeyIndex] = Node->Keys[Index];
    
    if(Child->Leaf == FALSE)
    {
        Child->Childs[Child->KeyIndex + 1] = Sibling->Childs[0];
    }

    Node->Keys[Index] = Sibling->Keys[0];

    for(int i = 1; i < Sibling->KeyIndex; i++)
    {
        Sibling->Keys[i - 1] = Sibling->Keys[i];
    }

    if(Sibling->Leaf == FALSE)
    {
        for(int i = 1; i < Sibling->KeyIndex; i++)
        {
            Sibling->Childs[i - 1] = Sibling->Childs[i];
        }
    }

    Child->KeyIndex++;
    Sibling->KeyIndex--;
}

void Merge(BTreeNode *Node, int Index)
{
    BTreeNode *Child = Node->Childs[Index];
    BTreeNode *Sibling = Node->Childs[Index + 1];

    Child->Keys[NODE_DEGREE - 1] = Node->Keys[Index];

    for(int i = 0; i < Sibling->KeyIndex; i++)
    {
        Child->Keys[i + NODE_DEGREE] = Sibling->Keys[i];
    }

    if(Child->Leaf == FALSE)
    {
        for (int i = 0; i < Sibling->KeyIndex + 1; i++)
        {
            Child->Childs[i + NODE_DEGREE] = Sibling->Childs[i];
        }
    }

    for (int i = Index + 1; i < Node->KeyIndex; i++)
    {
        Node->Keys[i - 1] = Node->Keys[i];
    }
    
    for (int i = Index + 2; i < Node->KeyIndex + 1; i++)
    {
        Node->Childs[i - 1] = Node->Childs[i];
    }
    
    Child->KeyIndex += Sibling->KeyIndex + 1;
    Node->KeyIndex--;
}

void PrintTree(BTreeNode *Node, int Level, int Blank)
{
    if(Node->KeyIndex == 0)
    {
        printf("Tree not exist\n");
        return;
    }

    if(Blank == TRUE)
    {
        for(int i = 0; i < Level; i++)
        {
            for(int j = 0; j < SPACE; j++)
            {
                printf(" ");
            }
            printf("|");
        }
    }

    if(Node->KeyIndex > 0)
    {
        for(int i = 0; i < (SPACE - 4 * Node->KeyIndex); i++)
        {
            printf(" ");
        }

        printf("[");
        for(int i = 0; i < Node->KeyIndex - 1; i++)
        {
            printf("%2d, ", Node->Keys[i]);
        }
        printf("%2d]|", Node->Keys[Node->KeyIndex - 1]);
    }
    else
    {
        printf("[ ]");
    }
    
    if(Node->Leaf == TRUE)
    {
        printf("\n");
        return;
    }
    else
    {
        for (int i = 0; i < Node->KeyIndex + 1; i++)
        {
            if(i != 0)
            {
                PrintTree(Node->Childs[i], Level + 1, TRUE);
            }
            else
            {
                PrintTree(Node->Childs[i], Level + 1, FALSE);
            }
        }        
    }    
}
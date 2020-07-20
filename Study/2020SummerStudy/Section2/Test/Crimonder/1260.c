#include <stdio.h>
#include <stdlib.h>

typedef struct Node {
    int id;
    struct Node* next;
} Node;

typedef struct {
    Node* Head;
    int numOfData;
    int (*Comp)(int data1, int data2);
} LinkedList;

void InitList(LinkedList* list) {
    list->Head = (Node*)malloc(sizeof(Node));
    list->Head->next = NULL;
    list->Comp = NULL;
    list->numOfData = 0;
}

void HeadInsert(LinkedList* list, int data) {
    Node* temp = (Node*)malloc(sizeof(Node));
    temp->id = data;

    temp->next = list->Head->next;
    list->Head->next = temp;

    list->numOfData++;
}

void SortInsert(LinkedList* list, int data) {
    Node* new = (Node*)malloc(sizeof(Node));
    Node* pred = list->Head;
    new->id = data;

    while((pred->next != NULL) && (list->Comp(data, pred->next->id) != 0)) {
        pred = pred->next;
    }

    new->next = pred->next;
    pred->next = new;

    list->numOfData++;
}

void HardInsert(LinkedList* list, int data) {
    Node* temp = (Node*)malloc(sizeof(Node));
    Node* last = list->Head;
    temp->id = data;

    while (last->next != NULL) {
        last = last->next;
    }

    last->next = temp;

    list->numOfData++;
}

void HardDelete(LinkedList* list) {
    Node* target = list->Head->next;
    Node* parent = list->Head;

    while (target->next != NULL) {
        parent = target;
        target = target->next;
    }

    parent->next = NULL;
    free(target);
    
    list->numOfData--;
}

void Insert(LinkedList* list, int data) {
    if (list->Comp == NULL) HeadInsert(list, data);
    else SortInsert(list, data);
}

int ComparePrecede(int data1, int data2) {
    if (data1 < data2) return 0;
    else return 1;
}

void SetSortRule(LinkedList* list, int (*Comp)(int data1, int data2)) {
    list->Comp = Comp;
}

bool HasNonStack(LinkedList* stack, LinkedList* list) {
    for (int i = 0; i < list->numOfData; i++) {
        
    }
}

void DFS(LinkedList[] list, int start) {
    LinkedList stack;
    InitList(&stack);

    int nextData = start;

    printf("%d ", nextData);
    HardInsert(&stack, nextData);

    while()
}

int main(){
    int numOfPoints;
    int numOfEdges;
    int startingPoint;

    int point1, point2;

    scanf("%d %d %d\n", *numOfPoints, *numOfEdges, *startingPoint);

    LinkedList list[numOfPoints];
    
    for(int i = 0; i < numOfPoints; i++) {
        InitList(&list[i]);
        SetSortRule(&list[i], ComparePrecede);
    }

    for(int i = 0; i < numOfPoints; i++) {
        scanf("%d %d\n", *point1, *point2);
        Insert(&list[point1 - 1], point2);
        Insert(&list[point2 - 1], point1);
    }


}
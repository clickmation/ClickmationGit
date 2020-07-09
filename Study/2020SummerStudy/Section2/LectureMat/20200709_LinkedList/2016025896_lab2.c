#include <stdio.h>
#include <stdlib.h>

typedef struct {
	int ID;
	char* Name;
} Student;

typedef struct Node {
	Student data;
	struct Node* next;
} Node;

typedef struct {
	Node* Head;
	int numOfData;
	int (*Comp)(int data1, int data2);// 함수포인터
} LinkedList;

void InitList(LinkedList* list){
	list->Head = (Node*)malloc(sizeof(Node));
	list->Head->next = NULL;
	list->Comp = NULL;
	list->numOfData = 0;
}

void HeadInsert(LinkedList* list, Student data) {
	Node* temp = (Node*)malloc(sizeof(Node));
	temp->data = data;

	temp->next = list->Head->next;
	list->Head->next = temp;
	
	list->numOfData++;
}

void SortInsert(LinkedList* list, Student data) {
	Node* new = (Node*)malloc(sizeof(Node));
	Node* pred = list->Head;
	new->data = data;

	while((pred->next != NULL) && (list->Comp(data.ID, pred->next->data.ID) != 0)) {
		pred = pred->next;
	}
	
	new->next = pred->next;
	pred->next = new;

	list->numOfData++;
}

void Insert(LinkedList* list, Student data){
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

Student* MakeStudent (int id, char* name) {
	Student* temp = (Student*)malloc(sizeof(Student));
	temp->ID = id;
	temp->Name = name;
	return temp;
}

void PrintAll(LinkedList* list){
	int num = list->numOfData;
	Node* temp = list->Head->next;
	printf("Num of datas %d\n", num);
	for (int i = 0; i < num; i++) {
		printf("ID : %d, Name : %s\n", temp->data.ID, temp->data.Name);
		temp = temp->next;
	}
}

void DeleteFromID(LinkedList* list, int id){
	int num = list->numOfData;
	Node* temp = list->Head;
	for (int i = 0; i < num; i++) {
		if (temp->next->data.ID == id) {
			Node* trash = temp->next;
			temp-> next = trash->next;
			free(trash);
			list->numOfData--;
			break;
		} else temp = temp->next;
	}
}

int main(){
	int nod = 5;

	LinkedList list;
	InitList(&list);
	SetSortRule(&list, ComparePrecede);
	
	Insert(&list, *MakeStudent(2020200001, "test person1"));
	Insert(&list, *MakeStudent(2020200765, "test person2"));
	Insert(&list, *MakeStudent(2020300004, "test person3"));
	Insert(&list, *MakeStudent(2020200628, "test person4"));
	Insert(&list, *MakeStudent(2020200218, "test person5"));

	PrintAll(&list);

	printf("\nDelete student node ID = 2020200628\n\n");
	DeleteFromID(&list, 2020200628);

	PrintAll(&list);
	


	return 0;
}

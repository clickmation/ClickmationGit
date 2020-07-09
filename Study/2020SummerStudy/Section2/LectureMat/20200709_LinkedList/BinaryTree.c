#include <stdio.h>
#include <stdlib.h>

#define COUNT 12

typedef int DATATYPE;

typedef struct BTNode
{
	DATATYPE data;
	struct BTNode* left;
	struct BTNode* right;
} BTNode;

BTNode* MakeBTNode(void);
DATATYPE RetData(BTNode* node);
void SaveData(BTNode* node, DATATYPE data);

BTNode* RetSubTreeLeft(BTNode* node);
BTNode* RetSubTreeRight(BTNode* node);

void MakeSubTreeLeft(BTNode* parent, BTNode* child);
void MakeSubTreeRight(BTNode* parent, BTNode* child);

BTNode* RemoveSubTreeLeft(BTNode* node);
BTNode* RemoveSubTreeRight(BTNode* node);

void ChangeSubTreeLeft(BTNode* parent, BTNode* child);
void ChangeSubTreeRight(BTNode* parent, BTNode* child);

void PreorderTraversal(BTNode* node);
void InorderTraversal(BTNode* node);
void PostorderTraversal(BTNode* node);

void MakeBST(BTNode** node);

void InsertBST(BTNode** root, DATATYPE data);
BTNode* SearchBST(BTNode* node, DATATYPE target);
BTNode* RemoveBST(BTNode** root, DATATYPE target);

void PrintAllBST(BTNode* node);

void Print2D(BTNode* root, int space);

int main(int argc, char *argv[]) {
	BTNode* root;
	BTNode* targetNode;
	int target;

	MakeBST(&root);

	InsertBST(&root, 8);
	InsertBST(&root, 4);
	InsertBST(&root, 10);
	InsertBST(&root, 9);
	InsertBST(&root, 2);
	InsertBST(&root, 11);
	InsertBST(&root, 6);
	InsertBST(&root, 1);
	InsertBST(&root, 7);
	InsertBST(&root, 9);
	InsertBST(&root, 2);
	InsertBST(&root, 11);
	InsertBST(&root, 6);
	InsertBST(&root, 1);
	InsertBST(&root, 7);
	InsertBST(&root, 5);
	InsertBST(&root, 12);
	InsertBST(&root, 3);

	PrintAllBST(root);
	printf("\n");

	target = 9;
	printf("Delete node has data : %d\n\n", target);
	targetNode = RemoveBST(&root, target);
	free(targetNode);
	PrintAllBST(root);
	printf("\n");

	target = 11;
	printf("Delete node has data : %d\n\n", target);
	targetNode = RemoveBST(&root, target);
	free(targetNode);
	PrintAllBST(root);
	printf("\n");

	target = 4;
	printf("Delete node has data : %d\n\n", target);
	targetNode = RemoveBST(&root, target);
	free(targetNode);
	PrintAllBST(root);
	printf("\n");

	target = 8;
	printf("Delete node has data : %d\n\n", target);
	targetNode = RemoveBST(&root, target);
	free(targetNode);
	PrintAllBST(root);
	printf("\n");

	return 0;
}

BTNode* MakeBTNode(void) {
	BTNode* node = (BTNode*)malloc(sizeof(BTNode));
	node->data = 0;
	node->left = NULL;
	node->right = NULL;
	return node;
}

DATATYPE RetData(BTNode* node) {
	return node->data;
}

void SaveData(BTNode* node, DATATYPE data) {
	node->data = data;
}

BTNode* RetSubTreeLeft(BTNode* node) {
	return node->left;
}

BTNode* RetSubTreeRight(BTNode* node) {
	return node->right;
}

void MakeSubTreeLeft(BTNode* parent, BTNode* child) {
	if(parent->left != NULL) free(parent->left);
	parent->left = child;
}

void MakeSubTreeRight(BTNode* parent, BTNode* child) {
	if(parent->right != NULL) free(parent->right);
	parent->right = child;
}

BTNode* RemoveSubTreeLeft(BTNode* node) {
	BTNode* temp;
	if(node != NULL) {
		temp = node->left;
		node->left = NULL;
	}

	return temp;
}

BTNode* RemoveSubTreeRight(BTNode* node) {
	BTNode* temp;
	if(node != NULL) {
		temp = node->right;
		node->right = NULL;
	}

	return temp;
}

void ChangeSubTreeLeft(BTNode* parent, BTNode* child) {
	parent->left = child;
}

void ChangeSubTreeRight(BTNode* parent, BTNode* child) {
	parent->right = child;
}

void PreorderTraversal(BTNode* node) {
	if(node	 == NULL) return;

	printf("%d", node->data);
	PreorderTraversal(node->left);
	PreorderTraversal(node->right);
}

void InorderTraversal(BTNode* node) {
	if(node	 == NULL) return;

	InorderTraversal(node->left);
	printf("%d", node->data);
	InorderTraversal(node->right);
}

void PostorderTraversal(BTNode* node) {
	if(node	 == NULL) return;

	PostorderTraversal(node->left);
	PostorderTraversal(node->right);
	printf("%d", node->data);
}

void MakeBST(BTNode** node) {
	*node = NULL;
}

void InsertBST(BTNode** root, DATATYPE data) {
	BTNode* parent = NULL;
	BTNode* current = *root;
	BTNode* temp = NULL;

	while(current != NULL) {
		if(data == RetData(current)) return;

		parent = current;
		if(RetData(current) > data) current = RetSubTreeLeft(current);
		else current = RetSubTreeRight(current);
	}

	temp = MakeBTNode();
	SaveData(temp, data);

	if(parent != NULL) {
		if(data < RetData(parent)) MakeSubTreeLeft(parent, temp);
		else MakeSubTreeRight(parent, temp);
	} else *root = temp;
}

BTNode* SearchBST(BTNode* node, DATATYPE target) {
	BTNode* current = node;
	DATATYPE data;

	while(current != NULL) {
		data = RetData(current);

		if(target == data) return current;
		else if(target < data) current = RetSubTreeLeft(current);
		else current = RetSubTreeRight(current);
	}

	return NULL;
}

BTNode* RemoveBST(BTNode** root, DATATYPE target) {
	BTNode* virtualRoot = MakeBTNode();

	BTNode* parent = virtualRoot;
	BTNode* current = *root;
	BTNode* targetNode;

	ChangeSubTreeRight(virtualRoot, *root);

	while(current != NULL && RetData(current) != target) {
		parent = current;

		if(target < RetData(current)) current = RetSubTreeLeft(current);
		else current = RetSubTreeRight(current);
	}

	if(current == NULL) return NULL;

	targetNode = current;

	if(RetSubTreeLeft(targetNode) == NULL && RetSubTreeRight(targetNode) == NULL) {
		if(RetSubTreeLeft(parent) == targetNode) RemoveSubTreeLeft(parent);
		else RemoveSubTreeRight(parent);
	}
	else if(RetSubTreeLeft(targetNode) == NULL || RetSubTreeRight(targetNode) == NULL) {
		BTNode* childOfTarget;

		if(RetSubTreeLeft(targetNode) != NULL) childOfTarget = RetSubTreeLeft(targetNode);
		else childOfTarget = RetSubTreeRight(targetNode);

		if(RetSubTreeLeft(parent) == targetNode) ChangeSubTreeLeft(parent, childOfTarget);
		else ChangeSubTreeRight(parent, childOfTarget);
	}
	else {
		BTNode* maximumNode = RetSubTreeLeft(targetNode);
		BTNode* parentOfMaximum = targetNode;

		DATATYPE backup;

		while(RetSubTreeRight(maximumNode) != NULL) {
			parentOfMaximum = maximumNode;
			maximumNode = RetSubTreeRight(maximumNode);
		}

		backup = RetData(targetNode);
		SaveData(targetNode, RetData(maximumNode));

		if(RetSubTreeRight(parentOfMaximum) == maximumNode) ChangeSubTreeRight(parentOfMaximum, RetSubTreeLeft(maximumNode));
		else ChangeSubTreeLeft(parentOfMaximum, RetSubTreeLeft(maximumNode));

		targetNode = maximumNode;
		SaveData(targetNode, backup);
	}

	if(RetSubTreeRight(virtualRoot) != *root) *root = RetSubTreeRight(virtualRoot);

	free(virtualRoot);
	return targetNode;
}

void PrintAllBST(BTNode* node) {
	printf("Nodes\n");
	InorderTraversal(node);
	printf("\n\nTree\n");
	Print2D(node, 0);
}

void Print2D(BTNode* root, int space) {
	if(root == NULL) return;

	space += COUNT;

	Print2D(root->right, space);
	printf("\n");
	for(int i = COUNT; i < space; i++) {
		printf(" ");
	}
	printf("%d\n", root->data);
	Print2D(root->left, space);
}
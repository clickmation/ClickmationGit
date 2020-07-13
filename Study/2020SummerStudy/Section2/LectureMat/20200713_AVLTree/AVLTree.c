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

BTNode* InsertBST(BTNode** root, DATATYPE data);
BTNode* SearchBST(BTNode* node, DATATYPE target);
BTNode* RemoveBST(BTNode** root, DATATYPE target);

BTNode* Rebalance(BTNode** root);

BTNode* RotateLL(BTNode* node);
BTNode* RotateRR(BTNode* node);
BTNode* RotateRL(BTNode* node);
BTNode* RotateLR(BTNode* node);

int RetHeight(BTNode* node);

int RetDiffInHeightOfSubTree(BTNode* node);

void PrintAllBST(BTNode* node);

void Print2D(BTNode* root, int space);

int main(int argc, char *argv[]) {
	BTNode* root;
	BTNode* targetNode;
	int target;

	MakeBST(&root);

	target = 5;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
	PrintAllBST(root);
	printf("\n");

	target = 4;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
	PrintAllBST(root);
	printf("\n");

	target = 3;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
	PrintAllBST(root);
	printf("\n");

	target = 1;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
	PrintAllBST(root);
	printf("\n");

	target = 2;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
	PrintAllBST(root);
	printf("\n");

	target = 6;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
	PrintAllBST(root);
	printf("\n");

	target = 7;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
	PrintAllBST(root);
	printf("\n");

	target = 9;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
	PrintAllBST(root);
	printf("\n");

	target = 8;
	printf("Insert %d\n", target);
	InsertBST(&root, target);
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

BTNode* InsertBST(BTNode** root, DATATYPE data) {
	if (*root == NULL) {
		*root = MakeBTNode();
		SaveData(*root, data);
	} else if (data < RetData(*root)) {
		InsertBST(&((*root)->left), data);
		*root = Rebalance(root);
	} else if (data > RetData(*root)) {
		InsertBST(&((*root)->right), data);
		*root = Rebalance(root);
	} else return NULL;

	return *root;
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

BTNode* Rebalance(BTNode** root) {
	int diff = RetDiffInHeightOfSubTree(*root);

	if (diff > 1) {
		if (RetDiffInHeightOfSubTree(RetSubTreeLeft(*root)) > 0) {
			printf("Rotate LL\n");
			*root = RotateLL(*root);
		} else {
			printf("Rotate LR\n");
			*root = RotateLR(*root);
		}
	} else if (diff < -1) {
		if (RetDiffInHeightOfSubTree(RetSubTreeRight(*root)) < 0) {
			printf("Rotate RR\n");
			*root = RotateRR(*root);
		} else {
			printf("Rotate RL\n");
			*root = RotateRL(*root);
		}
	}

	return *root;
}

BTNode* RotateLL(BTNode* node) {
	BTNode* parent;
	BTNode* child;

	parent = node;
	child = RetSubTreeLeft(parent);

	ChangeSubTreeLeft(parent, RetSubTreeRight(child));
	ChangeSubTreeRight(child, parent);

	return child;
}

BTNode* RotateRR(BTNode* node) {
	BTNode* parent;
	BTNode* child;

	parent = node;
	child = RetSubTreeRight(parent);

	ChangeSubTreeRight(parent, RetSubTreeLeft(child));
	ChangeSubTreeLeft(child, parent);

	return child;
}

BTNode* RotateRL(BTNode* node) {
	BTNode* parent;
	BTNode* child;

	parent = node;
	child = RetSubTreeRight(parent);

	ChangeSubTreeRight(parent, RotateLL(child));

	return RotateRR(parent);
}

BTNode* RotateLR(BTNode* node) {
	BTNode* parent;
	BTNode* child;

	parent = node;
	child = RetSubTreeLeft(parent);

	ChangeSubTreeLeft(parent, RotateRR(child));

	return RotateLL(parent);
}

int RetHeight(BTNode* node) {
	int HeightOfLeft;
	int HeightOfRight;

	if (node == NULL) return 0;

	HeightOfLeft = RetHeight(RetSubTreeLeft(node));
	HeightOfRight = RetHeight(RetSubTreeRight(node));

	if (HeightOfLeft > HeightOfRight) return HeightOfLeft + 1;
	else return HeightOfRight + 1;
}

int RetDiffInHeightOfSubTree(BTNode* node) {
	int HeightOfLeft;
	int HeightOfRight;

	if (node == NULL) return 0;

	HeightOfLeft = RetHeight(RetSubTreeLeft(node));
	HeightOfRight = RetHeight(RetSubTreeRight(node));

	return HeightOfLeft - HeightOfRight;
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
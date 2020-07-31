#include <iostream>
#include <stdio.h>
#include <queue>
#include <stack>
#include <algorithm>

#define MAX_SIZE 25

using namespace std;

int n;
int num_of_houses[MAX_SIZE * MAX_SIZE] = { 0, };
int house_number = 0;
int map_data[MAX_SIZE][MAX_SIZE];
bool visited[MAX_SIZE][MAX_SIZE];


// ��,��,��,��
int dx[4] = { 1, 0, -1, 0 };
int dy[4] = { 0, 1, 0, -1 };


// BFS
void bfs(int y, int x) {

    queue< pair<int, int> > q; // �̿��� ť, (x,y) -> (��, ��)
    q.push(make_pair(y, x)); // pair�� ���� queue�� �ֽ��ϴ�.

    // ó�� x,y�� �湮 �߱⶧����
    visited[y][x] = true;
    num_of_houses[house_number]++;

    while (!q.empty()) {

        // ť�� ���� ���Ҹ� ������
        y = q.front().first;
        x = q.front().second;
        q.pop();

        // �ش� ��ġ�� �ֺ��� Ȯ��
        for (int i = 0; i < 4; i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];

            // ������ ����� �ʰ�
            if (0 <= nx && nx < n && 0 <= ny && ny < n) {
                // ���̸鼭 �湮���� �ʾҴٸ� -> �湮
                if (map_data[ny][nx] == 1 && visited[ny][nx] == false) {
                    visited[ny][nx] = true;

                    // �ش� ������ ���� ���� ������Ŵ
                    num_of_houses[house_number]++;

                    // ť�� ���� nx,ny�� �߰�
                    q.push(make_pair(ny, nx));
                }
            }
        }
    }
}

int main() {
    scanf("%d", &n);

    // ���� ������ �Է�
    for (int col = 0; col < n; col++) {
        for (int raw = 0; raw < n; raw++) {
            scanf("%1d", &map_data[col][raw]);
        }
    }

    // ��ü�� ���� �����͸� �ϳ��ϳ� üũ
    for (int col = 0; col < n; col++) {
        for (int raw = 0; raw < n; raw++) {
            // ���� �����ϰ�, �湮���� �ʾ��� ��,
            if (map_data[col][raw] == 1 && visited[col][raw] == false) {
                house_number++;

                // bfs Ž�� ����
                bfs(col, raw);
            }
        }
    }

    // ������������ ����
    sort(num_of_houses + 1, num_of_houses + house_number + 1);

    // ���
    printf("%d\n", house_number);
    for (int i = 1; i <= house_number; i++) {
        printf("%d\n", num_of_houses[i]);
    }
    return 0;
}

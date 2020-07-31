#include <iostream>
#include <queue>

using namespace std;

int N, M;
int dx[] = { -1, 0, 1, 0 };
int dy[] = { 0, -1, 0, 1 };
int maze[101][101];

queue<pair<int, int>> q;

void bfs() {
    q.push(make_pair(0, 0));
    pair<int, int> current;
    int nx, ny;

    while (!q.empty()) {
        current = q.front();
        q.pop();

        for (int i = 0; i < 4; i++) {
            nx = current.second + dx[i];
            ny = current.first + dy[i];
        }
    }
}

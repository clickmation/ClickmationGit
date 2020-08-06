//내가 짬 ㅎ ㅎ

#include <cstdio>
#include <utility>
#include <queue>
#include <iostream>

using namespace std;

int dx[6] = {1, -1, 0, 0, 0, 0};
int dy[6] = {0, 0, 1, -1, 0, 0};
int dz[6] = {0, 0, 0, 0, 1, -1};

int tomato[101][101][101];
int d[101][101][101];

int n, m, h;

int main() {
    //cout << "7569 Åä¸¶Åä 3D\n";
    scanf("%d %d %d", &m, &n, &h);
    queue<pair<pair<int, int>, int>> q;
    for (int k = 0; k < h; k++) {
        for (int j = 0; j < n; j++) {
            for (int i = 0; i < m; i++) {
                scanf("%d", &tomato[i][j][k]);
                d[i][j][k] = -1;

                if (tomato[i][j][k] == 1) {
                    q.push({{i, j}, k});
                    d[i][j][k] = 0;
                }
            }
        }
    }

    while (!q.empty()) {
        int x = q.front().first.first;
        int y = q.front().first.second;
        int z = q.front().second;
        q.pop();

        for (int i = 0; i < 6; i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];
            int nz = z + dz[i];

            if (0 <= nx && nx < m && 0 <= ny && ny < n && 0 <= nz && nz < h) {
                if (tomato[nx][ny][nz] == 0 && d[nx][ny][nz] == -1) {
                    d[nx][ny][nz] = d[x][y][z] + 1;
                    q.push({{nx, ny}, nz});
                }
            }
        }
    }

    int result = 0;

    for (int i = 0; i < m; i++) {
        for (int j = 0; j < n; j++) {
            for (int k = 0; k < h; k++) {
                result = max(result, d[i][j][k]);
            }
        }
    }

    for (int i = 0; i < m; i++) {
        for (int j = 0; j < n; j++) {
            for (int k = 0; k < h; k++) {
                if (tomato[i][j][k] == 0 && d[i][j][k] == -1) {
                    result = -1;
                }
            }
        }
    }

    cout << result;
}

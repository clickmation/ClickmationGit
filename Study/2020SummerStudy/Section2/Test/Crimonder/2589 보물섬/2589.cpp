#include <cstdio>
#include <utility>
#include <queue>
#include <iostream>

using namespace std;

int dx[4] = {0, 0, 1, -1};
int dy[4] = {1, -1, 0, 0};
char b[51][51];
int d[51][51];

int w, h, maxMap, maxIsle;

pair<int, int> maxIsleLoc;

queue<pair<int, int>> q;

using namespace std;

void BFS1()
{
    while (!q.empty()) {
        int x = q.front().first;
        int y = q.front().second;
        q.pop();

        if (d[x][y] > maxIsle) {
            maxIsleLoc = {x, y};
        }

        for (int k = 0; k < 4; k++) {
            int nx = x + dx[k];
            int ny = y + dy[k];

            if (0 <= nx && nx < w && 0 <= ny && ny < h) {
                if (b[nx][ny] == 'L') {
                    b[nx][ny] = 'W';
                    d[nx][ny] = d[x][y] + 1;
                }
            }
        }
    }
}

void BFS2()
{
    while (!q.empty()) {
        int x = q.front().first;
        int y = q.front().second;
        q.pop();

        if (d[x][y] > maxMap) {
            maxMap = d[x][y];
        }

        for (int k = 0; k < 4; k++) {
            int nx = x + dx[k];
            int ny = y + dy[k];

            if (0 <= nx && nx < w && 0 <= ny && ny < h) {
                if (d[nx][ny] != -1) {
                    d[nx][ny] = d[x][y] + 1;
                }
            }
        }
    }
}

int main()
{
    scanf("%d %d", &h, &w);

    for (int j = 0; j < h; j++) {
        for (int i = 0; i < w; i++) {
            scanf(" %c", &b[i][j]);
            if (b[i][j] == 'W') {
                d[i][j] = -1;
            }
        }
    }

    for (int j = 0; j < h; j++) {
        for (int i = 0; i < w; i++) {
            if (b[i][j] == 'L') {
                q.push({i, j});
                BFS1();
                q.push({maxIsleLoc.first, maxIsleLoc.second});
                d[maxIsleLoc.first][maxIsleLoc.second] = 0;
                BFS2();
                maxIsle = 0;
            }
        }
    }

    cout << maxMap;
}

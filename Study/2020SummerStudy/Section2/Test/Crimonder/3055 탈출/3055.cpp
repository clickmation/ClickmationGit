#include <cstdio>
#include <utility>
#include <queue>
#include <iostream>

char b[51][51];
int d[51][51];
int dx[4] = {1, -1, 0, 0};
int dy[4] = {0, 0, 1, -1};

const int LARGENUM = 100;

int gx, gy;

using namespace std;

int main()
{
    int r, c;

    queue<pair<int, int>> dq;
    queue<pair<int, int>> wq;

    scanf("%d %d", &r, &c);

    for (int j = 0; j < r; j++) {
        for (int i = 0; i < c; i++) {
            scanf(" %c", &b[i][j]);

            if (b[i][j] == 'S') {
                dq.push({i, j});
            }
            else if (b[i][j] == '*') {
                d[i][j] = 0;
                wq.push({i, j});
            }
            else if (b[i][j] == 'X') {
                d[i][j] = 0;
            }
            else if (b[i][j] == 'D') {
                d[i][j] = LARGENUM;
                gx = i;
                gy = j;
            }
            else {
                d[i][j] = -1;
            }
        }
    }

    while (!wq.empty()) {
        int xd = wq.front().first;
        int yd = wq.front().second;
        wq.pop();

        for (int i = 0; i < 4; i++) {
            int nx = xd + dx[i];
            int ny = yd + dy[i];

            if (0 <= nx && nx < c && 0 <= ny && ny < r) {
                if (d[nx][ny] == -1) {
                    d[nx][ny] = d[xd][yd] + 1;
                    wq.push({nx, ny});
                }
            }
        }
    }

    while (!dq.empty()) {
        int xd = dq.front().first;
        int yd = dq.front().second;
        dq.pop();

        for (int i = 0; i < 4; i++) {
            int nx = xd + dx[i];
            int ny = yd + dy[i];

            if (0 <= nx && nx < c && 0 <= ny && ny < r) {
                 if (d[nx][ny] > d[xd][yd] + 1) {
                    d[nx][ny] = d[xd][yd] + 1;
                    if (d[gx][gy] < LARGENUM) {
                        cout << d[gx][gy];

                        return 0;
                    }
                    dq.push({nx, ny});
                 }
            }
        }
    }

    cout << "KAKTUS";

    return 0;
}

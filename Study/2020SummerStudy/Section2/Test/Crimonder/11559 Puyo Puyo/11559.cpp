#include <cstdio>
#include <utility>
#include <queue>
#include <iostream>

using namespace std;

int dx[4] = {1, -1, 0, 0};
int dy[4] = {0, 0, 1, -1};

const int HEIGHT = 12;
const int WIDTH = 6;

char b[WIDTH][HEIGHT];
int d[WIDTH][HEIGHT];

queue<pair<int, int>> q;
queue<pair<int, int>> tmp;

int combo = 0, cnt = 0, boom = 0;

void BFS() {

    while (!q.empty()) {
        int x = q.front().first;
        int y = q.front().second;
        q.pop();
        d[x][y] = 1;
        tmp.push({x, y});

        cnt++;

        for (int k = 0; k < 4; k++) {
            int nx = x + dx[k];
            int ny = y + dy[k];

            if (0 <= nx && nx < WIDTH && 0 <= ny && ny < HEIGHT) {
                if (d[nx][ny] == 0 && b[nx][ny] == b[x][y]) {
                    d[nx][ny] = 1;
                    q.push({nx, ny});
                }
            }
        }
    }

    if (cnt >= 4) {
        boom++;

        while (!tmp.empty()) {
            int tx = tmp.front().first;
            int ty = tmp.front().second;
            tmp.pop();
            b[tx][ty] = '.';
        }
    }

    cnt = 0;
}

void Collapse(int x, int y) {
    if(0 <= (y + 1) && (y + 1) < HEIGHT && b[x][y + 1] == '.') {
        b[x][y + 1] = b[x][y];
        b[x][y] = '.';
        Collapse(x, y + 1);
    }
}

int main()
{
    for (int j = 0; j < HEIGHT; j++) {
        for (int i = 0; i < WIDTH; i++) {
            scanf(" %c", &b[i][j]);
            if(b[i][j] == '.') {
                d[i][j] = 1;
            }
        }
    }

    for (int j = 0; j < HEIGHT; j++) {
        for (int i = 0; i < WIDTH; i++) {
            if (b[i][j] != '.' && d[i][j] == 0) {
                q.push({i, j});
                BFS();
            }
        }
    }

    if (boom == 0) {

        cout << combo;

        return 0;
    }

    for (int j = 0; j < HEIGHT; j++) {
        for (int i = 0; i < WIDTH; i++) {
            cout << b[i][j];
        }
        cout << endl;
    }

    for (int j = HEIGHT - 1; j >= 0; j--) {
        for (int i = 0; i < WIDTH; i++) {
            Collapse(i, j);
        }
    }

    for (int j = 0; j < HEIGHT; j++) {
        for (int i = 0; i < WIDTH; i++) {
            cout << b[i][j];
        }
        cout << endl;
    }

    return 0;
}

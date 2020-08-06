#include <iostream>

using namespace std;

int n;
char rgb[101][101];
int d[101][101] {0,};

int dx[4] = {1, -1, 0, 0};
int dy[4] = {0, 0, 1, -1};


void DFS (int x, int y, int v[101][101])
{
    v[x][y] = 1;

    for (int i = 0; i < 4; i++) {
        int nx = x + dx[i];
        int ny = y + dy[i];

        if (0 <= nx && nx < n && 0 <= ny && ny < n && rgb[nx][ny] == rgb[x][y]) {
            DFS(nx, ny, v);
        }
    }
}

void Remap (char c[101][101])
{
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (c[i][j] == 'G') {
                c[i][j] = 'R';
            }
        }
    }
}

int main()
{
    int cnt = 0;

    scanf("%d", &n);

    for (int i = 0; i < n; i++) {
        scanf("%s", &rgb[i][0]);
    }

    /*
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            cout << d[i][j];
        }
    }
    */

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (d[i][j] == 0) {
                DFS(i, j, d);
                cnt++;
            }
        }
    }

    cout << cnt << " ";

    Remap(rgb);

    cnt = 0;

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (d[i][j] != 1) {
                DFS(i, j, d);
                cnt++;
            }
        }
    }

    cout << cnt << endl;
}

//내가 짬 ㅎㅎ

#include <iostream>

using namespace std;

int n;
char rgb[101][101];
int d[101][101] = {0,};

int dx[4] = {1, -1, 0, 0};
int dy[4] = {0, 0, 1, -1};


void DFS (int x, int y)
{
    d[x][y] = 1;

    for (int i = 0; i < 4; i++) {
        int nx = x + dx[i];
        int ny = y + dy[i];

        if (0 <= nx && nx < n && 0 <= ny && ny < n && rgb[nx][ny] == rgb[x][y] && d[nx][ny] == 0) {
            DFS(nx, ny);
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

    /*
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            cout << d[i][j];
        }
    }
    */


    for (int i = 0; i < n; i++) {
        scanf("%s", &rgb[i][0]);
    }


    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (d[i][j] == 0) {
                DFS(i, j);
                cnt++;
            }
        }
    }

    //이걸 아예 d를 두개 만들어서 dfs에서 d를 받아오게 해도 되긴함 근데 쥰내 귀찮
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            d[i][j] = 0;
        }
    }

    cout << cnt << " ";

    Remap(rgb);

    cnt = 0;

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (d[i][j] != 1) {
                DFS(i, j);
                cnt++;
            }
        }
    }

    cout << cnt << endl;
}

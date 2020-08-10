#include <iostream>

using namespace std;

int x, n, m, sum = 0, cnt = 0, year = 0;

int offSet[301][301] = {0,};
int d[301][301] = {0,};
int board[301][301];

int dx[4] = {1, -1, 0, 0};
int dy[4] = {0, 0, 1, -1};

int DFS(int x, int y)
{
    d[x][y] = 1;

    for (int t = 0; t < 4; t++) {
        int nx = x + dx[t];
        int ny = y + dx[t];

        if (0 <= nx && nx < m && 0 <= ny && ny < n) {
            if (d[nx][ny] == 0) {
                DFS(nx, ny);
            }
        }
    }
}

int main()
{
    scanf("%d %d", &n, &m);

    for (int j = 0; j < n; j++) {
        for (int i = 0; i < m; i++) {
            scanf("%d", &board[i][j]);
            sum += board[i][j];
        }
    }

    while (cnt < 2) {

        if(sum == 0) {
            cout << sum;

            return 0;
        }

        cnt = 0;

        for (int j = 0; j < n; j++) {
            for (int i = 0; i < m; i++) {

                d[i][j] = 0;

                if (board[i][j] == 0) {
                    for (int t = 0; t < 4; t++) {
                        int nx = i + dx[t];
                        int ny = j + dy[t];

                        if (0 <= nx && nx < m && 0 <= ny && ny < n) {
                            if (0 != board[nx][ny] && offSet[nx][ny] < board[nx][ny]) {
                                offSet[nx][ny]++;
                            }
                        }
                    }
                }
            }
        }

        sum = 0;

        for (int j = 0; j < n; j++) {
            for (int i = 0; i < m; i++) {
                board[i][j] -= offSet[i][j];
                sum += board[i][j];
                offSet[i][j] = 0;
                if (board[i][j] == 0) {
                    d[i][j] = 1;
                }
            }
        }

        year++;

        for (int j = 0; j < n; j++) {
            for (int i = 0; i < m; i++) {
                cout << d[i][j] << " ";
            }
            cout << endl;
        }


        for (int j = 0; j < n; j++) {
            for (int i = 0; i < m; i++) {
                if (d[i][j] == 0) {
                    DFS(i, j);
                    printf("%d %d", i, j);
                    cnt++;
                }
            }
        }

        printf("count is %d", cnt);
        scanf("%d", x);
    }

    cout << year;

    return 0;
}

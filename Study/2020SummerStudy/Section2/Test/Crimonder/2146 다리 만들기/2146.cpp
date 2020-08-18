#include <cstdio>
#include <utility>
#include <queue>
#include <iostream>

int b[101][101];
int d[101][101];
int dx[4] = {1, -1, 0, 0};
int dy[4] = {0, 0, 1, -1};

int smallnum, n, isle = 0;

queue<pair<int, int>> q;


using namespace std;

void BFS()
{
    int t[101][101];

    while (!q.empty())
    {
        int x = q.front().first;
        int y = q.front().second;
        q.pop();

        t[x][y] = 1;

        b[x][y] = 0;

        for (int k = 0; k < 4; k++)
        {
            int nx = x + dx[k];
            int ny = y + dy[k];

            if (0 <= nx && nx < n && 0 <= ny && ny < n && t[nx][ny] == 0)
            {
                if (b[nx][ny] == 0)
                {
                    d[nx][ny] = d[x][y] + 1;
                    q.push({nx, ny});
                }
                else if (b[nx][ny] == 1)
                {
                    if (b[x][y] == 0)
                    {
                        if (0 < d[x][y] && d[x][y] < smallnum)
                        {
                            smallnum = d[x][y];
                        }
                    }
                    else
                    {
                        q.push({nx, ny});
                    }
                }
            }
        }
    }
}

int main()
{
    scanf("%d", &n);

    for (int j = 0; j < n; j++)
    {
        for (int i = 0; i < n; i++)
        {
            scanf("%d", &b[i][j]);
            if (b[i][j] == 0)
            {
                d[i][j] = 1;
            }
        }
    }

    for (int j = 0; j < n; j++)
    {
        for (int i = 0; i < n; i++)
        {
            if (b[i][j] == 1)
            {
                q.push({i, j});
                BFS();
            }
        }
    }

    cout << smallnum;
}

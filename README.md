
---

# Campo Minado — Jogo em C# no Console

## ▶️ Como Executar

1. **Clone ou baixe** este repositório.
2. **Abra o projeto** em um editor como Visual Studio ou Rider.
3. Compile e execute o projeto (a classe principal é `Game.cs`, método `startGame()`).
4. O jogo será exibido no terminal.
5. Escolha as coordenadas `Y` e `X` (linhas e colunas) para revelar blocos no campo.

---

## 🧠 Lógica do Jogo

Este jogo foi totalmente criado do zero, com algoritmos próprios para geração de bombas, detecção de bombas ao redor, e abertura de blocos em cadeia. Abaixo estão os principais componentes e como funcionam:

---

### 🔢 Algoritmo de Geração de Bombas

As bombas são geradas de forma aleatória, com aproximadamente 15% dos blocos sendo bombas. Para isso, usamos:

```csharp
var rand = new Random();
int randomNumberForX = rand.Next(0, x);
int randomNumberForY = rand.Next(0, y);
```

Cada bomba é representada por uma coordenada `(x, y)` e armazenada em uma lista. O algoritmo evita duplicatas: se uma coordenada já foi sorteada, ele tenta novamente.

---

### 💣 Objeto Bomba (Classe `Bloco`)

Cada célula do campo é um objeto `Bloco`, que tem os seguintes atributos:

* `isBomb` → Define se o bloco é uma bomba.
* `bombsArround` → Quantas bombas existem ao redor deste bloco.
* `isVisible` → Indica se o bloco foi revelado ou não.

Exemplo:

```csharp
Bloco bloco = new Bloco();
bloco.isBomb = true;
bloco.bombsArround = 2;
bloco.isVisible = false;
```

---

### 🧭 Algoritmo de Detecção de Bombas ao Redor

Depois que o campo é gerado, cada bloco verifica as 8 posições ao redor (cima, baixo, lados e diagonais) para contar quantas bombas estão por perto.

```csharp
int[] XYangulos = { -1, 0, 1 };

foreach (int YDirection in XYangulos)
{
    foreach (int xDirection in XYangulos)
    {
        if (campoMinado[yIndex + YDirection][xIndex + xDirection].isBomb)
        {
            bombCount++;
        }
    }
}
```

Este valor é armazenado em `bombsArround` de cada bloco. Assim, se um bloco tiver `3` bombas próximas, será mostrado como `[3]` quando revelado.

---

### 📦 Algoritmo de Clique no Bloco

Ao clicar em um bloco:

* Se ele for bomba → fim de jogo.
* Se ele tiver bombas próximas → só revela aquele bloco.
* Se **não** tiver bombas próximas → revela em cadeia os vizinhos (algoritmo de expansão).

A expansão é feita com uma fila de blocos a verificar:

```csharp
if (campoMinado[y][x].bombsArround == 0)
{
    foreach (int[] direcao in direcoes)
    {
        // Adiciona bloco vizinho se ainda não foi aberto
        blocosParaAbrir.Add(vizinho);
    }
}
```

Isso simula a revelação "em cascata", como no jogo original.

---

### 🧾 Impressão do Campo

Cada vez que o jogador escolhe uma coordenada, o campo é impresso no console. Os símbolos usados são:

* `[ ]` → Bloco revelado e vazio
* `[2]`, `[3]`, etc. → Bloco com bombas próximas
* `[💣]` → Bomba (só aparece ao perder)
* `[#]` → Bloco oculto

---

## ❌ Fim de Jogo

Se o jogador clicar em uma bomba, o jogo termina e todas as bombas são reveladas com:

```csharp
if (isBomb)
{
    mostrarTodasBombas(); // Mostra todas as bombas com emoji 💣
}
```

---

## ✅ O que esse projeto demonstra

* Uso de matrizes em C#
* Lógica de vizinhança (análise de blocos ao redor)
* Geração de coordenadas aleatórias únicas
* Controle de fluxo com loops e tratamento de erros
* Interação simples no console com o jogador

---


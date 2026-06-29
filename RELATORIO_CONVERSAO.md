# Relatório de Viabilidade de Conversão - Projeto OTLA

Este documento descreve a análise técnica do projeto OTLA e as recomendações para sua modernização e execução em versões atuais do Windows (x64).

## 1. Diagnóstico Técnico Atual

*   **Tecnologia de Origem:** Borland C++ Builder 6 (2002).
*   **Biblioteca de Interface:** VCL (Visual Component Library) - Proprietária e obsoleta para padrões modernos de interoperabilidade com Visual Studio.
*   **Causa Raiz da Incompatibilidade:**
    *   O executável principal é 32 bits (Win32), que tecnicamente roda em Windows x64.
    *   No entanto, o projeto depende de ferramentas externas como `msxr2b.exe` (localizado em `tools\msx_r2b\`), que é um binário de **16 bits**. O Windows 64 bits não possui suporte nativo (subsistema NTVDM) para rodar código de 16 bits, resultando no erro reportado.
*   **Lógica de Negócio:** Os algoritmos de parsing (`zxfiles.cpp`, `msxfiles.cpp`, `cpcfiles.cpp`, `81files.cpp`) e o motor de geração de áudio (`wav2.cpp`) são escritos em C++ razoavelmente padrão, facilitando a portabilidade.

## 2. Opções de Conversão

### Opção A: Reescrita Híbrida (Recomendada)
Esta opção foca em longevidade e facilidade de manutenção futura utilizando o Visual Studio Professional.

*   **Back-end (Lógica):** Converter os arquivos de processamento (`funciones.cpp`, `sbb.cpp`, `loaders.cpp`, `wav2.cpp`, etc.) em uma **DLL C++ Nativa** compilada no Visual Studio.
    *   *Ajustes necessários:* Substituir `<mem.h>` por `<string.h>`, portar `AnsiString` para `std::string` ou `wchar_t*`, e remover diretivas `#pragma` específicas da Borland.
*   **Front-end (UI):** Desenvolver uma nova interface em **C# (Windows Forms ou WPF)**.
    *   *Vantagens:* Desenvolvimento rápido de UI, suporte total a resoluções modernas, facilidade de uso de bibliotecas de áudio como **NAudio** para o monitor de ondas.
*   **Interoperabilidade:** O C# chama as funções da DLL C++ via **P/Invoke**.

### Opção B: Atualização no Ecossistema Embarcadero
*   Migrar o código para o **Embarcadero C++Builder 12 (Athens)**.
*   *Vantagens:* Permite reaproveitar 90% do código de interface (`.dfm`), convertendo-o automaticamente para VCL moderna. Suporta compilação Win64 nativa.
*   *Desvantagem:* Requer licenciamento específico da Embarcadero.

## 3. Plano de Ação para Visual Studio e C#

1.  **Isolamento da Lógica:**
    *   Criar um projeto "Class Library (Native C++)" no Visual Studio.
    *   Adicionar os arquivos de processamento de formatos e geração de áudio.
    *   Exportar as funções principais (ex: `sbb2wav`, `leetap`, etc.) usando `extern "C" __declspec(dllexport)`.
2.  **Substituição de Dependências de 16 bits:**
    *   O `msxr2b.exe` deve ser substituído por uma função em C++ que realize o parsing do cabeçalho da ROM MSX e extraia o binário, eliminando a dependência externa.
    *   O `lame.exe` deve ser atualizado para a versão de 32 ou 64 bits mais recente.
3.  **Desenvolvimento da UI em C#:**
    *   Mapear os campos do formulário original para controles equivalentes no .NET.
    *   Implementar a lógica de "Add Blocks" invocando a DLL de lógica.
4.  **Modernização do Player de Áudio:**
    *   Substituir a dependência de `mmsystem.h` (`waveOut`) no C++ pela biblioteca **NAudio** no C#, que oferece melhor controle sobre dispositivos de áudio e visualização de sinal (Waveform).

## 4. Conclusão de Viabilidade

A conversão é **totalmente viável** e o código-fonte atual possui uma boa separação de responsabilidades. O maior impedimento atual (erro de 16 bits) é pontual e fácil de resolver substituindo os utilitários na pasta `tools`. A migração para C# com lógica em C++ trará ao projeto a modernidade necessária para rodar de forma estável no Windows 10/11.

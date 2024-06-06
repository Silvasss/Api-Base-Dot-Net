using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class SoliciticacoesRepository(IConfiguration config) : ISoliciticacoesRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg => { cfg.CreateMap<Solicitacao, SolicitacaoDto>().ReverseMap(); }));

        public async Task<IEnumerable<SolicitacaoDto>> Get(int id)
        {
            return _mapper.Map<IEnumerable<SolicitacaoDto>>(await _entityFramework.Solicitacao.Where(s => s.Instituicao_Id == id).ToListAsync());
        }

        public async Task<SolicitacaoDto> GetSolicitacao(int id, int InstituicaoId)
        {
            return _mapper.Map<SolicitacaoDto>(await _entityFramework.Solicitacao.Where(s => s.Solicitacao_Id == id && s.Instituicao_Id == InstituicaoId).FirstOrDefaultAsync());
        }

        public async Task<bool> Put(SolicitacaoDto solicitacao, int id)
        {
            Solicitacao? solicitacaoDb = await _entityFramework.Solicitacao.Where(s => s.Solicitacao_Id == solicitacao.Solicitacao_Id && s.Instituicao_Id == id).FirstOrDefaultAsync();

            if (solicitacaoDb != null)
            {
                solicitacaoDb.Descricao = solicitacao.Descricao;
                solicitacaoDb.Ativo = solicitacao.Ativo;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

//As situações que uma matrícula de um estudante de curso superior pode ter são variadas e dependem de diferentes fatores e procedimentos administrativos. Abaixo estão as principais situações de matrícula, conforme descrito nas fontes:

//### Situações de Matrícula

//1. **Matriculado**: O aluno está oficialmente vinculado a um curso e deve iniciar a frequência às aulas e atividades do curso, conforme o início do semestre letivo[5][7].

//2. **Trancado**: O aluno solicitou o trancamento da matrícula, interrompendo temporariamente o vínculo com o curso. Esta situação pode ser revertida quando o aluno decide retomar os estudos[3][7].

//3. **Concludente**: O aluno completou a carga horária total do curso e está em processo de finalização, como estágio ou colação de grau[3].

//4. **Concluído**: O aluno integralizou a carga horária e cumpriu todos os requisitos do curso, mas ainda não colou grau[3].

//5. **Transferido Externo**: O aluno solicitou transferência para outra instituição de ensino[3].

//6. **Transferido Interno**: O aluno solicitou mudança de curso dentro da mesma instituição. Esta situação pode envolver a análise das disciplinas já cursadas[3].

//7. **Falecido**: A matrícula é encerrada devido ao falecimento do aluno[3].

//8. **Cancelamento Compulsório**: A matrícula é cancelada por motivos não admitidos pela instituição, como infrações graves[3].

//### Situações Periódicas de Matrícula

//1. **Aprovado**: O aluno foi aprovado em todas as disciplinas cursadas em um determinado período letivo. Esta situação é atualizada no fechamento do período[3].

//2. **Aprovado com Dependência**: O aluno foi aprovado no período letivo, mas ainda tem disciplinas pendentes de períodos anteriores[3].

//3. **Reprovado**: O aluno não obteve aproveitamento suficiente em uma ou mais disciplinas, resultando em reprovação[2][3].

//4. **Aluno em Dependência**: O aluno está matriculado em disciplinas pendentes de períodos anteriores ou em programas especiais para recuperação[2].

//5. **Aluno em Adaptação Curricular**: O aluno está matriculado em disciplinas ou turmas diferentes da sua matrícula regular, geralmente devido a reestruturações curriculares[2].

//### Outras Situações

//1. **Aluno Especial**: Matriculado em disciplinas isoladas ou parte de cursos, conforme normas da instituição[2].

//2. **Aluno Ouvinte**: Matriculado apenas para assistir às aulas sem a obrigatoriedade de cumprir frequência ou obter notas[2].

//3. **Aluno Formado**: Cumpriu todos os componentes curriculares obrigatórios do curso e está regular com o ENADE, mas ainda não colou grau[2].

//Estas situações refletem a diversidade de estados em que um aluno pode se encontrar durante seu percurso acadêmico, cada uma com implicações específicas para a continuidade ou interrupção dos estudos.

//### Situações de Matrícula

//1. **Matriculado**: O aluno está oficialmente vinculado a um curso e deve iniciar a frequência às aulas e atividades do curso, conforme o início do semestre letivo[5][7].

//2. **Trancado**: O aluno solicitou o trancamento da matrícula, interrompendo temporariamente o vínculo com o curso. Esta situação pode ser revertida quando o aluno decide retomar os estudos[3][7].

//3. **Concludente**: O aluno completou a carga horária total do curso e está em processo de finalização, como estágio ou colação de grau[3].

//4. **Concluído**: O aluno integralizou a carga horária e cumpriu todos os requisitos do curso, mas ainda não colou grau[3].

//5. **Transferido Externo**: O aluno solicitou transferência para outra instituição de ensino[3].

//6. **Transferido Interno**: O aluno solicitou mudança de curso dentro da mesma instituição. Esta situação pode envolver a análise das disciplinas já cursadas[3].

//7. **Falecido**: A matrícula é encerrada devido ao falecimento do aluno[3].

//8. **Cancelamento Compulsório**: A matrícula é cancelada por motivos não admitidos pela instituição, como infrações graves[3].

//### Situações Periódicas de Matrícula

//1. **Aprovado**: O aluno foi aprovado em todas as disciplinas cursadas em um determinado período letivo. Esta situação é atualizada no fechamento do período[3].

//2. **Aprovado com Dependência**: O aluno foi aprovado no período letivo, mas ainda tem disciplinas pendentes de períodos anteriores[3].

//3. **Reprovado**: O aluno não obteve aproveitamento suficiente em uma ou mais disciplinas, resultando em reprovação[2][3].

//4. **Aluno em Dependência**: O aluno está matriculado em disciplinas pendentes de períodos anteriores ou em programas especiais para recuperação[2].

//5. **Aluno em Adaptação Curricular**: O aluno está matriculado em disciplinas ou turmas diferentes da sua matrícula regular, geralmente devido a reestruturações curriculares[2].

//### Outras Situações

//1. **Aluno Especial**: Matriculado em disciplinas isoladas ou parte de cursos, conforme normas da instituição[2].

//2. **Aluno Ouvinte**: Matriculado apenas para assistir às aulas sem a obrigatoriedade de cumprir frequência ou obter notas[2].

//3. **Aluno Formado**: Cumpriu todos os componentes curriculares obrigatórios do curso e está regular com o ENADE, mas ainda não colou grau[2].

//Estas situações refletem a diversidade de estados em que um aluno pode se encontrar durante seu percurso acadêmico, cada uma com implicações específicas para a continuidade ou interrupção dos estudos.


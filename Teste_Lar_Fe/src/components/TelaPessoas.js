// TelaPessoas.js
import axios from 'axios';
import React, { useEffect, useState } from 'react';
import './TelaPessoas.css'; // Importe o arquivo de estilos CSS

function TelaPessoas({ onEditar, onDeletar }) {
  const [pessoas, setPessoas] = useState([]);
  const [termoPesquisa, setTermoPesquisa] = useState('');

  useEffect(() => {
    const fetchData = async () => {
      const token = localStorage.getItem('token');
      const response = await axios.get('http://localhost:5109/Pessoa', {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
      setPessoas(response.data);
    };

    fetchData();
  }, []);

  const pessoasFiltradas = pessoas.filter(pessoa =>
    pessoa.nome.toLowerCase().includes(termoPesquisa.toLowerCase()) ||
    pessoa.cpf.includes(termoPesquisa)
    // Adicione mais critérios conforme necessário
  );

  return (
    <div className="tela-pessoas-container">
        <h2>Pesquisar pessoa por qualquer informção:</h2>
      <div className="pesquisa">
        <input
          type="text"
          className="form-control"
          placeholder="Pesquisar Pessoa"
          value={termoPesquisa}
          onChange={(e) => setTermoPesquisa(e.target.value)}
        />
      </div>
      <br></br>
      <table className="table table-hover">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Ativo</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoasFiltradas.map(pessoa => (
            <tr key={pessoa.id}>
              <td>{pessoa.nome}</td>
              <td>{pessoa.estaAtivo ? 'Sim' : 'Não'}</td>
              <td>
                <button className="btn btn-primary me-2" onClick={() => onEditar(pessoa)}>Editar</button>
                <button className="btn btn-danger" onClick={() => onDeletar(pessoa.id)}>Deletar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default TelaPessoas;

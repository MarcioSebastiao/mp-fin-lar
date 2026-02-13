import CriarPessoa from "../../componentes/Pessoa/CriarPessoa";
import Modal from "../../componentes/Modal";
import { useState } from "react";
import "./Pessoas.css";

function Pessoas() {
    const [modalAberto, setModalAberto] = useState(false);
    return (
        <>
            <Modal aberto={modalAberto} aoFechar={() => setModalAberto(false)}>
                <CriarPessoa sucessoAoCriar={() => setModalAberto(false)} />
            </Modal>
            <div className="container">
                <div>
                    <button onClick={() => setModalAberto(true)}>Nova Pessoa</button>
                </div>
            </div>
        </>
    );
}

export default Pessoas;

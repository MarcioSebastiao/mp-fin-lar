import CriarPessoa from "../../componentes/Pessoa/CriarPessoa";
import Modal from "../../componentes/Modal";
import { useState } from "react";

function Pessoas() {
    const [modalAberto, setModalAberto] = useState(false);
    return (
        <>
            <button onClick={() => setModalAberto(true)}>Nova Pessoa</button>
            <Modal aberto={modalAberto} aoFechar={() => setModalAberto(false)}>
                <CriarPessoa sucessoAoCriar={() => setModalAberto(false)} />
            </Modal>
        </>
    );
}

export default Pessoas;

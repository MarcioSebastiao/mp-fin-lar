import React, { useEffect } from "react";
import "./Modal.css";

interface ModalProps {
    aberto: boolean;
    aoFechar: () => void;
    children: React.ReactNode;
    tituloBotaoAbrir: string;
}

function Modal({ aberto, aoFechar, children, tituloBotaoAbrir }: ModalProps) {
    const [abertoInterno, setAbertoInterno] = React.useState(aberto);

    useEffect(() => {
        setAbertoInterno(aberto);
    }, [aberto]);
    return (
        <>
            {abertoInterno && (
                <div className="modal" onClick={() => { setAbertoInterno(false); aoFechar(); }}>
                    <div className="modal-conteudo" onClick={(e) => e.stopPropagation()}>
                        <div>
                            <button className="modal-fechar" onClick={() => { setAbertoInterno(false); aoFechar(); }}>
                                x
                            </button>
                        </div>
                        {children}
                    </div>
                </div>
            )}

            <div className="botao-abrir-modal">
                <button onClick={() => setAbertoInterno(true)}>{tituloBotaoAbrir}</button>
            </div>
        </>
    );
}
export default Modal;

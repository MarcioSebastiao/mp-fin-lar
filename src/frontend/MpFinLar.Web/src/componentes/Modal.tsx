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
                <div
                    className="modal"
                    onClick={() => {
                        setAbertoInterno(false);
                        aoFechar();
                    }}
                >
                    <div className="modal-conteudo" onClick={(e) => e.stopPropagation()}>
                        <div>
                            <button
                                className="modal-fechar"
                                onClick={() => {
                                    setAbertoInterno(false);
                                    aoFechar();
                                }}
                            >
                                <svg
                                    xmlns="http://www.w3.org/2000/svg"
                                    fill="none"
                                    viewBox="0 0 24 24"
                                    strokeWidth={1.5}
                                    stroke="currentColor"
                                    className="icone fechar"
                                >
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M6 18 18 6M6 6l12 12" />
                                </svg>
                            </button>
                        </div>
                        {children}
                    </div>
                </div>
            )}

            <div className="abrir-modal">
                <button className="botao" onClick={() => setAbertoInterno(true)}>
                    {tituloBotaoAbrir}
                </button>
            </div>
        </>
    );
}
export default Modal;

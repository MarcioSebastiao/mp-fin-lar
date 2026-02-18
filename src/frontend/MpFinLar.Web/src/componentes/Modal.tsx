import React from "react";
import "./Modal.css";

interface ModalProps {
    aberto: boolean;
    aoFechar: () => void;
    children: React.ReactNode;
}

function Modal({ aberto, aoFechar, children }: ModalProps) {
    if (!aberto) return null;

    return (
        <>
            <div
                className="modal"
                onClick={() => {
                    aoFechar();
                }}
            >
                <div className="modal-conteudo" onClick={(e) => e.stopPropagation()}>
                    <div>
                        <button
                            className="modal-fechar"
                            onClick={() => {
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
        </>
    );
}
export default Modal;

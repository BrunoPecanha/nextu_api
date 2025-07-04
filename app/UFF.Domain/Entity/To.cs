﻿using System;

namespace UFF.Domain.Entity
{
    public class To {
        /// <summary>
        /// Id do registro
        /// </summary>       
        public int Id { get; private set; }
        /// <summary>
        /// Data de cadastro do registro.
        /// </summary>
        public DateTime RegisteringDate { get; protected set; }
        /// <summary>
        /// Data da última alteração no registro da cena
        /// </summary>
        public DateTime LastUpdate { get; protected set; }
    }
}
